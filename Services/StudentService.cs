using Data.Interfaces.Repositories;
using Services.Interfaces;

namespace Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task DoActivityAsync(int activityId, int studentId, CancellationToken ct)
    {
        //TODO: add validations
        await _studentRepository.DoActivityAsync(activityId, studentId, ct);
    }
}