using Npgsql;
using Microsoft.Extensions.Configuration;

public class DatabaseConfig
{
    private readonly IConfiguration _configuration;

    public DatabaseConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public NpgsqlConnection GetUsersConnection()
    {
        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var db = Environment.GetEnvironmentVariable("DB_USERS");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password}";
        return new NpgsqlConnection(connectionString);
    }

    public NpgsqlConnection GetCommentsConnection()
    {
        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var db = Environment.GetEnvironmentVariable("DB_COMMENTS");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password}";
        return new NpgsqlConnection(connectionString);
    }
}
