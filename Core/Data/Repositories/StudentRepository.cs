using System.Data;
using Data.Helpers;
using Data.Interfaces;
using Data.Interfaces.Repositories;

namespace Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly IConnectionManager _connectionManager;

    public StudentRepository(IConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task DoActivityAsync(int activityId, int studentId, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                update User_Activity
                set IsDone = 1
                from User_Activity ua
                join [User] u on ua.UserId = u.Id
                where UserId = @studentId and u.[Role] = 2
                and ua.ActivityId = @activityId")
            .AddParameter("@studentId", studentId, DbType.Int32)
            .AddParameter("@activityId", activityId, DbType.Int32)
            .ExecuteAsync();
    }
}