using COMMON;

namespace DBHelper;
public class Utilities
{
    public static MySql.Data.MySqlClient.MySqlConnection GetOpenConnection()
    {
        string connectionString = SpiderSingleton.GetInstance.GetConnectionString();
        MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}