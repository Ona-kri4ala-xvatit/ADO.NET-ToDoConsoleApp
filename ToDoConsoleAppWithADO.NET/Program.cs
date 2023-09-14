using Microsoft.Data.SqlClient;
using ToDoConsoleAppWithADO.NET.Models;

const string server = "localhost\\SQLEXPRESS";
const string database = "ToDoListDB";
const string connectionString =
    $"Server = {server}; Database = {database}; Integrated Security=True; TrustServerCertificate=True;";

var tasks = new List<ToDoTask>();

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        while (true)
        {
            Console.WriteLine($"1\tShow Tasks\n2\tCreate Task\n3\tUpdate Task\n4\tDelete Task\nEsc\tExit");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();

                    CrudOperations.ReadTasks(connectionString, tasks);

                    foreach (var item in tasks)
                    {
                        Console.WriteLine(item);
                    }

                    break;

                case ConsoleKey.D2:
                    Console.Clear();

                    Console.Write("Enter task: ");
                    string tempTaskName = Console.ReadLine();

                    if (string.IsNullOrEmpty(tempTaskName))
                        continue;

                    CrudOperations.CreateTask(connectionString, new ToDoTask() { Name = tempTaskName });
                    Console.WriteLine("Task created\n\n");
                    break;

                case ConsoleKey.D3:
                    Console.Clear();



                    break;

                case ConsoleKey.D4:
                    Console.Clear();

                    Console.Write("Enter task id: ");
                    int tempTaskId = int.Parse(Console.ReadLine());

                    CrudOperations.DeleteTask(connectionString, tempTaskId);
                    Console.WriteLine("Task deleted\n\n");
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
