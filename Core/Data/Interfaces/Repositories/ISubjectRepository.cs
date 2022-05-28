namespace Data.Interfaces.Repositories;

public interface ISubjectRepository
{
    Task<int> CreateSubjectAsync(string name, CancellationToken ct);
    Task<int> SetProfessorForSubject(int userId, int subjectId, CancellationToken ct);
}