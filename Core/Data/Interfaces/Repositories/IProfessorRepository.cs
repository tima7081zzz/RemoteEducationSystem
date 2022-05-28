using Data.DTO;

namespace Data.Interfaces.Repositories;

public interface IProfessorRepository
{
    Task<int> CreateGroupAsync(string name, int userId, CancellationToken ct);
    Task AddUserToGroupAsync(int userId, int groupId, CancellationToken ct);
    Task AddActivityToSubject(ActivityDto activityModel, CancellationToken ct);
}