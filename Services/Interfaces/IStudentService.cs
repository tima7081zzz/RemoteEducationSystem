namespace Services.Interfaces;

public interface IStudentService
{
    Task DoActivityAsync(int activityId, int studentId, CancellationToken ct);
}