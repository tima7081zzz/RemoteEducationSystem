namespace Data.Interfaces.Repositories;

public interface IStudentRepository
{
    Task DoActivityAsync(int activityId, int studentId, CancellationToken ct);
}