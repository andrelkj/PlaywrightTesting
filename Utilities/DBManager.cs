using MySql.Data.MySqlClient;

namespace PlayWrightTesting.utilities;

internal class DBManager
{
    public static MySqlConnection connection;


    public static void SetMySQLDBConnection()
    {
        var connectionString = "server=127.0.0.1;user=root;password=selenium;database=a1b2c3d4";
        connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            Console.WriteLine("MySQL database connection established.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error establishing MySQL database connection: " + ex.Message);
        }
    }


    public static List<string> GetQuery(string query)
    {
        var results = new List<string>();

        try
        {
            using (var command = new MySqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Assuming the SELECT statement returns a single string column
                    var value = reader.GetString(0);
                    results.Add(value);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error executing SELECT statement: " + ex.Message);
        }

        return results;
    }
}