using System.Data;
using Dapper;
using Data.DTO;
using Data.DTO.Create;
using Data.Helpers;
using Data.Interfaces;
using Data.Interfaces.Repositories;

namespace Data.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly IConnectionManager _connectionManager;

    public SubjectRepository(IConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<int> CreateSubjectAsync(string name, CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into [Subject]([Name])
                output inserted.Id
                values(@name)")
            .AddParameter("@name", name, DbType.String)
            .QuerySingleOrDefault<int>();
    }

    public async Task<int> SetProfessorForSubjectAsync(int userId, int subjectId, CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into Professor(UserId, SubjectId)
                output inserted.Id
                values(@userId, @subjectId)")
            .AddParameter("userId", userId, DbType.Int32)
            .AddParameter("subjectId", subjectId, DbType.Int32)
            .QuerySingleOrDefault<int>();
    }

    public async Task<IEnumerable<ResourceDto>> GetAllResourcesAsync(CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .ReadOnly()
            .CancelWhen(ct)
            .UseQuery(@"
                select 
                    r.Id,
                    r.[Type],
                    r.[Name],           
                    r.SubjectId,
                    s.[Name] as SubjectName
                from [Resource] r
                join [Subject] s on r.SubjectId = s.Id")
            .QueryAsync<ResourceDto>();
    }

    public async Task<IEnumerable<CreateActivityDto>> GetAllActivitiesAsync(CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .ReadOnly()
            .CancelWhen(ct)
            .UseQuery(@"
                select 
                    a.Id,
                    a.[Type],
                    a.[Name],
                    a.MaxGrade,
                    a.SubjectId,
                    s.[Name] as SubjectName
                from Activity a
                join [Subject] s on a.SubjectId = s.Id")
            .QueryAsync<CreateActivityDto>();
    }

    public async Task<IEnumerable<SubjectFullDto>> GetAllSubjectsAsync(CancellationToken ct)
    {
        var joinDictionary = new Dictionary<int, SubjectFullDto>();
        const string query = @"
                select s.Id, s.[Name], u.Fullname
                from [Subject] s
                left join Professor p on p.SubjectId = s.Id
                left join [User] u on u.Id = p.UserId";

        using var connectionScope = _connectionManager.GetReadConnection();
        var connection = connectionScope.DbConnection;

        await connection.QueryAsync<SubjectFullDto, string, SubjectFullDto>(query, (dto, s) =>
        {
            if (!joinDictionary.TryGetValue(dto.Id, out var subject))
            {
                joinDictionary.Add(dto.Id, subject = dto);
            }

            subject.ProfessorsNames ??= new List<string>();

            if (s == null)
            {
                return subject;
            }

            subject.ProfessorsNames.Add(s);

            return subject;
        }, splitOn: "Fullname");

        return joinDictionary.Values;
    }
}