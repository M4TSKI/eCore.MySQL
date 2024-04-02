using MySqlConnector;

namespace eCore.MySQL;

public class Database(string connectionString)
{
    private readonly string dbConnectionString = connectionString;

    public async Task<MySqlConnection>GetConnectionAsync()
    {
        try
        {
            MySqlConnection connection = new(dbConnectionString);
            await connection.OpenAsync();
            return connection;
        }
        catch(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[eCore.MySQL] {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }

    public async Task<MySqlConnection> OnConnectionAsync(Func<MySqlConnection, Task> action)
    {
        using MySqlConnection connection = await GetConnectionAsync();
        try
        {
            await action(connection);
        }
        finally
        {
            await connection.CloseAsync();
        }
        return connection;
    }


    public MySqlConnection GetConnectionSync()
    {
        ;
        try
        {
            MySqlConnection? connection = null;
            connection = new MySqlConnection(dbConnectionString);
            connection.Open();
            return connection;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[eCore.MySQL] {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }

    public void OnConnectionSync(Action<MySqlConnection> action)
    {
        using (MySqlConnection connection = GetConnectionSync())
        {
            try
            {
                action(connection);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[eCore.MySQL] {ex.Message}");
                Console.ResetColor();
                throw;
            }           
            finally
            {
                connection.Close();
            }
        }
    }
    
}
