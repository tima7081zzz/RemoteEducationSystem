using System.Data;
using Data.Interfaces;

namespace Persistence.Configuration;

public class ConnectionScope : IConnectionScope
{
    public IDbConnection DbConnection { get; }
    public IDbTransaction DbTransaction { get; private set; }

    public ConnectionScope(IDbConnection dbConnection)
    {
        DbConnection = dbConnection;
    }

    public void BeginTransaction()
    {
        DbTransaction ??= DbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
    }

    public void RollBack()
    {
        if (DbConnection != null && DbConnection.State == ConnectionState.Open)
        {
            DbTransaction?.Rollback();
            DbTransaction?.Dispose();
        }
    }

    public void Commit()
    {
        if (DbConnection != null && DbConnection.State == ConnectionState.Open)
        {
            DbTransaction?.Commit();
            DbTransaction?.Dispose();
        }

        DbTransaction = null;
    }

    public void Dispose()
    {
        DbTransaction?.Dispose();
        DbConnection?.Dispose();
    }
}