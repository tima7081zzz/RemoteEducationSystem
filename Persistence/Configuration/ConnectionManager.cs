using Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Persistence.Configuration;

public class ConnectionManager : IConnectionManager
{
    private readonly IConfiguration _configuration;

    public ConnectionManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IConnectionScope GetConnection()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        return new ConnectionScope(sqlConnection);
    }

    public IConnectionScope GetReadConnection()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        return new ConnectionScope(sqlConnection);
    }
}