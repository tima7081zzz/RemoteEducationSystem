using Data.DTO;

namespace Data.Interfaces.Repositories;

public interface IStudentRepository
{
    Task DoActivityAsync(int activityId, int studentId, CancellationToken ct);
    Task<IEnumerable<ActivityDto>> GetStudentsActivitiesAsync(int userId, CancellationToken ct);
}