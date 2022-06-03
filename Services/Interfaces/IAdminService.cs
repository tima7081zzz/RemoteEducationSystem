using Data.DTO;

namespace Services.Interfaces;

public interface IAdminService
{
    Task<UserDto> AddUserAsync(CreateUserDto userModel, CancellationToken ct);
    Task<int> CreateSubjectAsync(string name, CancellationToken ct);
    Task<UserDto?> GetUserAsync(LoginUserDto loginUserModel, CancellationToken ct);
}