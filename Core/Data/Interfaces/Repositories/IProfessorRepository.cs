using Data.DTO;
using Data.DTO.Create;

namespace Data.Interfaces.Repositories;

public interface IProfessorRepository
{
    Task<int> CreateGroupAsync(string name, int userId, CancellationToken ct);
    Task AddUserToGroupAsync(int userId, int groupId, CancellationToken ct);
    Task<int> AddActivityToSubject(CreateActivityDto createActivityModel, CancellationToken ct);
    Task AddResourceToSubjectAsync(CreateResourceDto resourceModel, CancellationToken ct);
    Task RateStudentsActivityAsync(int studentId, int activityId, int grade, CancellationToken ct);
    Task<IEnumerable<GroupFullDto>> GetAllGroupsByProfessorIdAsync(int professorId, CancellationToken ct);
    Task SetProfessorForGroupAsync(int groupId, int professorId, CancellationToken ct);
    Task<IEnumerable<SubjectDto>> GetAllProfessorsSubjectsAsync(int professorId, CancellationToken ct);
    Task AddSubjectToGroupAsync(int groupId, int subjectId, int professorId, CancellationToken ct);
    Task<IEnumerable<UserDto>> GetAllStudentsAsync(CancellationToken ct);
    Task AddActivitiesForStudentsOfGroup(int professorId, int activityId, CancellationToken ct);
    Task<IEnumerable<ActivityDto>> GetAllProfessorsActivitiesToRate(int professorId, CancellationToken ct);
}