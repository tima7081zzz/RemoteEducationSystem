using Data.DTO;
using Data.DTO.Create;

namespace Data.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserDto?> GetUserByEmailAndPasswordAsync(string email, string password, CancellationToken ct);
    Task<int> CreateUserAsync(CreateUserDto userModel, CancellationToken ct);
    Task<UserDto> GetUserByIdAsync(int userId, CancellationToken ct);
    Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken ct);
    Task AddProfessorAsync(int professorId, CancellationToken ct);
    Task<IEnumerable<UserDto>> GetAllProfessorsAsync(CancellationToken ct);
}