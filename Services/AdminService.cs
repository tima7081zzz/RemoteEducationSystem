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

    public async Task<int> RegisterUserAsync(CreateUserDto userModel, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByEmailAndPasswordAsync(userModel.Email, userModel.Password, ct);
        if (user != null)
        {
            throw new AlreadyExistsException(nameof(User), userModel.Email);
        }

        return await _userRepository.CreateUserAsync(userModel, ct);
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