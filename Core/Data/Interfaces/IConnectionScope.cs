using System.Data;

namespace Data.Interfaces;

public interface IConnectionScope : IDisposable
{
    IDbConnection DbConnection { get; }
    IDbTransaction DbTransaction { get; }
    void BeginTransaction();
    void RollBack();
    void Commit();
}