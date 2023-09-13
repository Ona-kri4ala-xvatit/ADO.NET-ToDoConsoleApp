using Microsoft.Data.SqlClient;

namespace ToDoConsoleAppWithADO.NET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string server = "localhost\\SQLEXPRESS";
            const string database = "ToDoListDB";
            const string connectionString = $"Server = {server}; Database = {database}; Integrated Security=True;TrustServerCertificate=True";

            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                var commandText = "Select * from Tasks";
                var command = new SqlCommand(commandText, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader[0]}\nTask: {reader[1]}\n");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}