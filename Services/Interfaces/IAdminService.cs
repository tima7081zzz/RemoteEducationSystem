using Data.DTO;
using Data.DTO.Create;

namespace Services.Interfaces;

public interface IAdminService
{
    Task<UserDto> AddUserAsync(CreateUserDto userModel, CancellationToken ct);
    Task<int> CreateSubjectAsync(string name, CancellationToken ct);
    Task<UserDto?> GetUserAsync(LoginUserDto loginUserModel, CancellationToken ct);
    Task<IEnumerable<UserDto>> GetAllUsersAsync(int currentUserId, CancellationToken ct);
    Task<IEnumerable<ResourceDto>> GetAllResourcesAsync(int currentUserId, CancellationToken ct);
    Task<IEnumerable<CreateActivityDto>> GetAllActivitiesAsync(int currentUserId, CancellationToken ct);

}