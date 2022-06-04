using Data.DTO;

namespace Services.Interfaces;

public interface IStudentService
{
    Task DoActivityAsync(int activityId, int studentId, CancellationToken ct);
    Task<IEnumerable<ActivityDto>> GetStudentsActivitiesAsync(int userId, CancellationToken ct);
}