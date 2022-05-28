using Data.DTO;
using Data.Interfaces.Repositories;
using Domain;
using Services.Common.Exceptions;
using Services.Interfaces;

namespace Services;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;

    public AdminService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> RegisterUserAsync(CreateUserDto userModel, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByEmailAndPasswordAsync(userModel.Email, userModel.Password, ct);
        if (user != null)
        {
            throw new AlreadyExistsException(nameof(User), userModel.Email);
        }

        return await _userRepository.CreateUserAsync(userModel, ct);
    }
}