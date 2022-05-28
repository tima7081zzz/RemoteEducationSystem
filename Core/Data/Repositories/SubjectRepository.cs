using System.Data;
using Data.DTO;
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

    public async Task<int> SetProfessorForSubject(int userId, int subjectId, CancellationToken ct)
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
}