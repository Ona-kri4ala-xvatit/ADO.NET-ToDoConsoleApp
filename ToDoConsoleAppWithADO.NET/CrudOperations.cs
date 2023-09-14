using Microsoft.Data.SqlClient;
using ToDoConsoleAppWithADO.NET.Models;

public static class CrudOperations
{
    public static void ReadTasks(string connectionString, List<ToDoTask> tasks)
    {
        tasks.Clear();
        var commandText = "Select * from Tasks";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new ToDoTask() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        //Console.WriteLine($"ID: {reader[0]}\nTask: {reader[1]}\n");
                    }
                }
            }
        }
    }

    public static void CreateTask(string connectionString, ToDoTask newTasks)
    {
        var commandText = $"Insert into Tasks([Name]) Values('{newTasks.Name}')";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                command.ExecuteReader();
            }
        }
    }

    public static void UpdateTask(string connectionString, ToDoTask newTasks)
    {
        var commantText = $"Update ";
    }

    public static void DeleteTask(string connectionString, int id)
    {
        var commandText = $"Delete from Tasks Where [Id] = ({id})";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                command.ExecuteReader();
            }
        }
    }
}


