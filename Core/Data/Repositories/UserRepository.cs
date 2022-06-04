using System.Data;
using Data.DTO;
using Data.DTO.Create;
using Data.Helpers;
using Data.Interfaces;
using Data.Interfaces.Repositories;

namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IConnectionManager _connectionManager;

    public UserRepository(IConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<UserDto?> GetUserByEmailAndPasswordAsync(string email, string password, CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .ReadOnly()
            .CancelWhen(ct)
            .UseQuery(@"
                select Id, [Role], Fullname, Email
                from [User] 
                where Email = @email and [Password] = @password")
            .AddParameter("@email", email, DbType.String)
            .AddParameter("@password", password, DbType.String)
            .QuerySingleOrDefault<UserDto>();
    }

    public async Task<int> CreateUserAsync(CreateUserDto userModel, CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into [User](Email, [Password], Fullname, [Role])
                output inserted.Id
                values(@email, @password, @fullname, @role)")
            .AddParameter("@email", userModel.Email, DbType.String)
            .AddParameter("@password", userModel.Password, DbType.String)
            .AddParameter("@fullname", userModel.Fullname, DbType.String)
            .AddParameter("@role", userModel.Role, DbType.Byte)
            .QuerySingleOrDefault<int>();
    }

    public async Task AddProfessorAsync(int professorId, CancellationToken ct)
    {
        await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .CancelWhen(ct)
            .UseQuery(@"
                insert into Professor(UserId)
                values(@professorId)")
            .AddParameter("@professorId", professorId, DbType.Int32)
            .ExecuteAsync();
    }

    public async Task<UserDto> GetUserByIdAsync(int userId, CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .ReadOnly()
            .CancelWhen(ct)
            .UseQuery(@"
                select Id, [Role], Fullname, Email
                from [User] 
                where Id = @userId")
            .AddParameter("@userId", userId, DbType.Int32)
            .QuerySingleOrDefault<UserDto>();
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .ReadOnly()
            .CancelWhen(ct)
            .UseQuery(@"
                select * from [User]
                where [Role] <> 0")
            .QueryAsync<UserDto>();
    }

    public async Task<IEnumerable<UserDto>> GetAllProfessorsAsync(CancellationToken ct)
    {
        return await QueryExecutionBuilder
            .ForConnectionManager(_connectionManager)
            .ReadOnly()
            .CancelWhen(ct)
            .UseQuery(@"
                select * from [User]
                where [Role] = 1")
            .QueryAsync<UserDto>();
    }
}