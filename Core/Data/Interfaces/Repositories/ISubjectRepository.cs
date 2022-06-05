using Data.DTO;
using Data.DTO.Create;

namespace Data.Interfaces.Repositories;

public interface ISubjectRepository
{
    Task<int> CreateSubjectAsync(string name, CancellationToken ct);
    Task<int> SetProfessorForSubjectAsync(int userId, int subjectId, CancellationToken ct);
    Task<IEnumerable<ResourceDto>> GetAllResourcesAsync(CancellationToken ct);
    Task<IEnumerable<CreateActivityDto>> GetAllActivitiesAsync(CancellationToken ct);
    Task<IEnumerable<SubjectFullDto>> GetAllSubjectsAsync(CancellationToken ct);
}