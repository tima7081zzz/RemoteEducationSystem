using System.Data;
using Dapper;
using Data.Interfaces;

namespace Data.Helpers;

public class QueryExecutionBuilder
{
    private readonly IConnectionManager _connectionManager;
    private string _query;
    private DynamicParameters _parameters;
    private CancellationToken _cancellationToken = CancellationToken.None;
    private bool _isReadoly;


    private QueryExecutionBuilder(IConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public static QueryExecutionBuilder ForConnectionManager(IConnectionManager connectionManager)
    {
        return new QueryExecutionBuilder(connectionManager);
    }

    public QueryExecutionBuilder ReadOnly()
    {
        _isReadoly = true;
        return this;
    }

    public QueryExecutionBuilder UseQuery(string query)
    {
        _query = query;
        return this;
    }

    public QueryExecutionBuilder UseParameters(DynamicParameters parameters)
    {
        _parameters = parameters;
        return this;
    }

    public QueryExecutionBuilder AddParameter(string name, object value, DbType? dbType)
    {
        _parameters ??= new DynamicParameters();
        _parameters.Add(name, value, dbType);
        return this;
    }

    public QueryExecutionBuilder CancelWhen(CancellationToken ct)
    {
        _cancellationToken = ct;
        return this;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>()
    {
        using var connectionScope = PrepareConnection();
        return await connectionScope.DbConnection.QueryAsync<T>(PrepareCommand(connectionScope));
    }

    public async Task<T> ExecuteScalar<T>()
    {
        using var connectionScope = PrepareConnection();
        return await connectionScope.DbConnection.ExecuteScalarAsync<T>(PrepareCommand(connectionScope));
    }

    public async Task<T> QuerySingleOrDefault<T>()
    {
        using var connectionScope = PrepareConnection();
        try
        {
            var result = await connectionScope.DbConnection.QuerySingleOrDefaultAsync<T>(PrepareCommand(connectionScope));
            connectionScope.Commit();

            return result;
        }
        catch
        {
            connectionScope.RollBack();
            throw;
        }
    }

    public async Task ExecuteAsync()
    {
        using var connectionScope = PrepareConnection();
        try
        {
            await connectionScope.DbConnection.ExecuteAsync(PrepareCommand(connectionScope));

            connectionScope.Commit();
        }
        catch
        {
            connectionScope.RollBack();
        }
    }

    private IConnectionScope PrepareConnection()
    {
        return _isReadoly ? _connectionManager.GetReadConnection() : _connectionManager.GetConnection();
    }

    private CommandDefinition PrepareCommand(IConnectionScope scope)
    {
        _parameters ??= new DynamicParameters();

        return new CommandDefinition(_query, _parameters, scope.DbTransaction, cancellationToken: _cancellationToken);
    }
}