using Data.DTO;
using Data.Interfaces.Repositories;
using Domain;
using Domain.Enums;
using Services.Common.Exceptions;
using Services.Interfaces;

namespace Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;

    public StudentService(IStudentRepository studentRepository, IUserRepository userRepository)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
    }

    public async Task DoActivityAsync(int activityId, int studentId, CancellationToken ct)
    {
        await ValidateUser(studentId, ct);

        await _studentRepository.DoActivityAsync(activityId, studentId, ct);
    }

    public async Task<IEnumerable<ActivityDto>> GetStudentsActivitiesAsync(int userId, CancellationToken ct)
    {
        await ValidateUser(userId, ct);

        return await _studentRepository.GetStudentsActivitiesAsync(userId, ct);
    }

    private async Task ValidateUser(int userId, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByIdAsync(userId, ct);
        if (user?.Role != EUserRole.Student)
        {
            throw new WrongOperationException(nameof(User), user!.Role);
        }
    }
}