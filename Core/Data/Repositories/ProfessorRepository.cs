using System.Data;
using Data.DTO;
using Data.DTO.Create;
using Data.Helpers;
using Data.Interfaces;
using Data.Interfaces.Repositories;
using Domain;

namespace Data.Repositories;

public class ProfessorRepository : IProfessorRepository
{
    private readonly IConnectionManager _connectionManager;

    public ProfessorRepository(IConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<int> CreateGroupAsync(string name, int userId, CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into [Group]([Name])
                output inserted.Id
                values(@name)")
            .AddParameter("@name", name, DbType.String)
            .AddParameter("@userId", userId, DbType.Int32)
            .QuerySingleOrDefault<int>();
    }

    public async Task AddUserToGroupAsync(int userId, int groupId, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into User_Group(UserId, GroupId)
                values(@userId, @groupId)")
            .AddParameter("@userId", userId, DbType.Int32)
            .AddParameter("@groupId", groupId, DbType.Int32)
            .ExecuteAsync();
    }

    public async Task<IEnumerable<GroupDto>> GetAllGroupsByProfessorIdAsync(int professorId, CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .ReadOnly()
            .UseQuery(@"
                select * 
                from [Group] g
                join Professor p on p.GroupId = g.Id
                where p.UserId = @professorId")
            .AddParameter("@professorId", professorId, DbType.Int32)
            .QueryAsync<GroupDto>();
    }

    public async Task AddActivityToSubject(CreateActivityDto createActivityModel, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into Activity([Type], [Name], MaxGrade, SubjectId)
                output inserted.Id
                values(@type, @name, @maxGrade, @subjectId)")
            .AddParameter("@subjectId", createActivityModel.SubjectId, DbType.Int32)
            .AddParameter("@name", createActivityModel.Name, DbType.String)
            .AddParameter("@type", createActivityModel.Type, DbType.Byte)
            .AddParameter("@maxGrade", createActivityModel.MaxGrade, DbType.Int32)
            .ExecuteAsync();
    }

    public async Task AddResourceToSubjectAsync(CreateResourceDto resourceModel, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into [Resource]([Type], [Name], SubjectId)
                output inserted.Id
                values(@type, @name, @subjectId)")
            .AddParameter("@type", resourceModel.Type, DbType.Byte)
            .AddParameter("@subjectId", resourceModel.SubjectId, DbType.Int32)
            .AddParameter("@name", resourceModel.Name, DbType.String)
            .ExecuteAsync();
    }

    public async Task RateStudentsActivityAsync(int studentId, int activityId, int grade, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                update User_Activity
                set Grade = @grade
                from User_Activity ua
                join [User] u on ua.UserId = u.Id
                where UserId = @studentId and u.[Role] = 2
                    and ua.ActivityId = @activityId")
            .AddParameter("@studentId", studentId, DbType.Int32)
            .AddParameter("@grade", grade, DbType.Int32)
            .AddParameter("@activityId", activityId, DbType.Int32)
            .ExecuteAsync();
    }

    public async Task SetProfessorForGroupAsync(int groupId, int professorId, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into Professor(UserId, GroupId)
                values(@professorId, @groupId)")
            .AddParameter("@groupId", groupId, DbType.Int32)
            .AddParameter("@professorId", professorId, DbType.Int32)
            .ExecuteAsync();
    }
}