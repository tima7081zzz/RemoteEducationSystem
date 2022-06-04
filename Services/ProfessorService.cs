using Data.DTO;
using Data.DTO.Create;
using Data.Interfaces.Repositories;
using Services.Common.Exceptions;
using Services.Interfaces;

namespace Services;

public class ProfessorService : IProfessorService
{
    private readonly IProfessorRepository _professorRepository;

    public ProfessorService(IProfessorRepository professorRepository)
    {
        _professorRepository = professorRepository;
    }

    public async Task<int> CreateGroupAsync(string name, int userId, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyFieldException(nameof(name));
        }

        return await _professorRepository.CreateGroupAsync(name, userId, ct);
    }

    public async Task AddUserToGroupAsync(int userId, int groupId, CancellationToken ct)
    {
        //TODO: add validations

        await _professorRepository.AddUserToGroupAsync(userId, groupId, ct);
    }

    public async Task AddActivityToSubject(int userId, CreateActivityDto createActivityModel, CancellationToken ct)
    {
        //TODO: add validations
        await _professorRepository.AddActivityToSubject(createActivityModel, ct);
    }

    public async Task AddResourceToSubjectAsync(CreateResourceDto resourceModel, CancellationToken ct)
    {
        //TODO: add validations
        await _professorRepository.AddResourceToSubjectAsync(resourceModel, ct);
    }

    public async Task RateStudentsActivityAsync(int studentId, int activityId, int grade, CancellationToken ct)
    {
        //TODO: add validations
        await _professorRepository.RateStudentsActivityAsync(studentId, activityId, grade, ct);
    }

    public async Task<IEnumerable<GroupDto>> GetAllGroupsByProfessorIdAsync(int professorId, CancellationToken ct)
    {
        return await _professorRepository.GetAllGroupsByProfessorIdAsync(professorId, ct);
    }

}