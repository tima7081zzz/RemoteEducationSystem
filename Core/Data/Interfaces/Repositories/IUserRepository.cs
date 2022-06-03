using Data.DTO;

namespace Data.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserDto?> GetUserByEmailAndPasswordAsync(string email, string password, CancellationToken ct);
    Task<int> CreateUserAsync(CreateUserDto userModel, CancellationToken ct);
    Task<UserDto> GetUserByIdAsync(int userId, CancellationToken ct);
}