using Data.DTO;

namespace Services.Interfaces;

public interface IProfessorService
{
    Task<int> CreateGroupAsync(string name, int userId, CancellationToken ct);
    Task AddUserToGroupAsync(int userId, int groupId, CancellationToken ct);
    Task AddActivityToSubject(int userId, ActivityDto activityModel, CancellationToken ct);
}