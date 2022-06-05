using System.Data;
using Dapper;
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

    public async Task<IEnumerable<SubjectDto>> GetAllProfessorsSubjectsAsync(int professorId, CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .ReadOnly()
            .CancelWhen(ct)
            .UseQuery(@"
                select s.*
                from Professor p
                join [Subject] s on s.Id = p.SubjectId
                where p.UserId = @professorId")
            .AddParameter("@professorId", professorId, DbType.Int32)
            .QueryAsync<SubjectDto>();
    }

    public async Task<IEnumerable<GroupFullDto>> GetAllGroupsByProfessorIdAsync(int professorId, CancellationToken ct)
    {
        var joinDictionary = new Dictionary<int, GroupFullDto>();
        const string query = @"
                select g.Id, g.[Name], u.Fullname, s.[Name]
                from [Group] g
                left join Professor p on p.GroupId = g.Id
                left join User_Group ug on ug.GroupId = g.Id
                left join [User] u on u.Id = ug.UserId
                left join [Subject] s on s.Id = p.SubjectId
                where p.UserId = @professorId";

        var parameters = new DynamicParameters();
        parameters.Add("@professorId", professorId, DbType.Int32);

        using var connectionScope = _connectionManager.GetReadConnection();
        var connection = connectionScope.DbConnection;

        var cmd = new CommandDefinition(query, parameters, connectionScope.DbTransaction, cancellationToken: ct);

        await connection.QueryAsync<GroupFullDto, string, string, GroupFullDto>(cmd, (dto, studentName, subjectName) =>
        {
            if (!joinDictionary.TryGetValue(dto.Id, out var subject))
            {
                joinDictionary.Add(dto.Id, subject = dto);
            }

            subject.StudentsNames ??= new List<string>();
            subject.SubjectNames ??= new List<string>();


            if (studentName != null)
            {
                subject.StudentsNames.Add(studentName);
            }

            if (subjectName != null)
            {
                subject.SubjectNames.Add(subjectName);
            }

            return subject;
        }, splitOn: "Fullname, Name");

        return joinDictionary.Values;
    }

    public async Task<int> AddActivityToSubject(CreateActivityDto createActivityModel, CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into Activity([Type], [Name], MaxGrade, SubjectId, ProfessorId)
                output inserted.Id
                values(@type, @name, @maxGrade, @subjectId, @professorId)")
            .AddParameter("@subjectId", createActivityModel.SubjectId, DbType.Int32)
            .AddParameter("@name", createActivityModel.Name, DbType.String)
            .AddParameter("@type", createActivityModel.Type, DbType.Byte)
            .AddParameter("@maxGrade", createActivityModel.MaxGrade, DbType.Int32)
            .AddParameter("@professorId", createActivityModel.ProfessorId, DbType.Int32)
            .ExecuteScalar<int>();
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

    public async Task AddStudentToGroupAsync(int groupId, int studentId, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into User_Group(UserId, GroupId)
                values(@studentId, @groupId)")
            .AddParameter("@groupId", groupId, DbType.Int32)
            .AddParameter("@studentId", studentId, DbType.Int32)
            .ExecuteAsync();
    }

    public async Task AddSubjectToGroupAsync(int groupId, int subjectId, int professorId, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                update Professor
                set SubjectId = @subjectId
                where UserId = @professorId and GroupId = @groupId")
            .AddParameter("@groupId", groupId, DbType.Int32)
            .AddParameter("@subjectId", subjectId, DbType.Int32)
            .AddParameter("@professorId", professorId, DbType.Int32)
            .ExecuteAsync();
    }

    public async Task<IEnumerable<UserDto>> GetAllStudentsAsync(CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .ReadOnly()
            .UseQuery(@"
                select * from [User] 
                where [Role] = 2")
            .QueryAsync<UserDto>();
    }

    public async Task AddActivitiesForStudentsOfGroup(int professorId, int activityId, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into User_Activity(UserId, ActivityId, [Date])
                select u.Id, @activityId, @date 
                from Professor p
                join User_Group ug
                	on ug.GroupId = p.GroupId
                join [User] u
                	on u.Id = ug.UserId
                where p.UserId = @professorId")
            .AddParameter("@professorId", professorId, DbType.Int32)
            .AddParameter("@activityId", activityId, DbType.Int32)
            .AddParameter("@date", DateTime.Now, DbType.Date)
            .ExecuteAsync();
    }
}