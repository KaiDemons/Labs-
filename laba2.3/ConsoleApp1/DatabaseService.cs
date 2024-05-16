using Npgsql;

namespace DailyPlanner;

public class DatabaseService
{
    private static NpgsqlConnection? _connection;
    private static string GetConnectionString()
    {
        //return @"Host=10.30.0.137;Port=5432;Database=gr624_praev;Username=gr624_praev;Password=Cfifmr2005!";
        return @"Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";
    }
    
    public static NpgsqlConnection GetSqlConnection()
    {
        if (_connection is null)
        {
            _connection = new NpgsqlConnection(GetConnectionString());
            _connection.Open();
        }
        
        return _connection;
    }
}