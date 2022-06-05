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
    Task<IEnumerable<GroupFullDto>> GetAllGroupsByProfessorIdAsync(int professorId, CancellationToken ct);
    Task<IEnumerable<SubjectDto>> GetAllProfessorsSubjectsAsync(int professorId, CancellationToken ct);
    Task AddSubjectToGroupAsync(int groupId, int subjectId, int professorId, CancellationToken ct);
    Task<IEnumerable<UserDto>> GetAllStudentsAsync(CancellationToken ct);
    Task<IEnumerable<ActivityDto>> GetAllProfessorsActivitiesToRate(int professorId, CancellationToken ct);
}