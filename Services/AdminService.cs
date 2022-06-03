using Data.DTO;
using Data.Interfaces.Repositories;
using Domain;
using Domain.Enums;
using Services.Common.Exceptions;
using Services.Interfaces;

namespace Services;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly ISubjectRepository _subjectRepository ;

    public AdminService(IUserRepository userRepository, ISubjectRepository subjectRepository)
    {
        _userRepository = userRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<UserDto> AddUserAsync(CreateUserDto userModel, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByEmailAndPasswordAsync(userModel.Email, userModel.Password, ct);
        if (user != null)
        {
            throw new AlreadyExistsException(nameof(User), userModel.Email);
        }

        var userId = await _userRepository.CreateUserAsync(userModel, ct);
        return await _userRepository.GetUserByIdAsync(userId, ct);
    }

    public async Task<UserDto?> GetUserAsync(LoginUserDto loginUserModel, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(loginUserModel.Email) || string.IsNullOrWhiteSpace(loginUserModel.Password))
        {
            return null;
        }

        return await _userRepository.GetUserByEmailAndPasswordAsync(loginUserModel.Email, loginUserModel.Password, ct);
    }

    public async Task<int> CreateSubjectAsync(string name, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyFieldException(nameof(name));
        }

        return await _subjectRepository.CreateSubjectAsync(name, ct);
    }

    public async Task<int> SetProfessorForSubject(int userId, int subjectId, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByIdAsync(userId, ct);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        if (user.Role != EUserRole.Professor)
        {
            throw new WrongOperationException(nameof(User), user.Role);
        }

        //TODO: add checking of existing this user for this subject
        //add all validations

        return await _subjectRepository.SetProfessorForSubject(userId, subjectId, ct);
    }
}