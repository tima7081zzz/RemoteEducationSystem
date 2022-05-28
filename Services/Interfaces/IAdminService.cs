using Data.DTO;

namespace Services.Interfaces;

public interface IAdminService
{
    Task<int> RegisterUserAsync(CreateUserDto userModel, CancellationToken ct);
    Task<int> CreateSubjectAsync(string name, CancellationToken ct);
}