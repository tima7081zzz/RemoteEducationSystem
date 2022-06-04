using Data.DTO;
using Data.DTO.Create;

namespace Services.Interfaces;

public interface IProfessorService
{
    Task<int> CreateGroupAsync(string name, int userId, CancellationToken ct);
    Task AddUserToGroupAsync(int userId, int groupId, CancellationToken ct);
    Task AddActivityToSubject(int userId, CreateActivityDto createActivityModel, CancellationToken ct);
    Task AddResourceToSubjectAsync(CreateResourceDto resourceModel, CancellationToken ct);
    Task RateStudentsActivityAsync(int studentId, int activityId, int grade, CancellationToken ct);
    Task<IEnumerable<GroupDto>> GetAllGroupsByProfessorIdAsync(int professorId, CancellationToken ct);
}