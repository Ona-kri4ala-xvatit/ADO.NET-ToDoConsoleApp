using System.Linq;
using ToDoConsoleAppWithADO.NET.Models;

const string server = "localhost\\SQLEXPRESS";
const string database = "ToDoListDB";
const string connectionString =
    $"Server = {server}; Database = {database}; Integrated Security=True; TrustServerCertificate=True;";

ToDoTaskRepository repository = new ToDoTaskRepository(connectionString);

while (true)
{
    Console.WriteLine($@"  
  _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 
 |                                 |
 |   Press to activate             |
 |                                 |
 |   1 - Show Tasks                |
 |   2 - Create Task               |
 |   3 - Update Task               |
 |   4 - Delete Task               |
 |   Esc - Exit                    |
 |_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _|");

    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.D1:
            Console.Clear();

            var tasks = repository.ReadTasks();
            foreach (var item in tasks)
            {
                Console.WriteLine(item);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press 'enter' to continue");
            Console.ResetColor();
            break;

        case ConsoleKey.D2:
            Console.Clear();

            Console.Write("Enter Task: ");
            string tempTaskName = Console.ReadLine()!;

            Console.Write("Priority (ex. Low, High, Critical): ");
            string tempTaskPriority = Console.ReadLine()!;

            Console.Write("Status (ex. Not Started, In Progress, Complete): ");
            string tempTaskStatus = Console.ReadLine()!;

            //if (string.IsNullOrEmpty(tempTaskName) || string.IsNullOrEmpty(tempTaskPriority) || string.IsNullOrEmpty(tempTaskStatus))
            //    continue;

            ToDoTask task = new ToDoTask()
            {
                TaskName = tempTaskName,
                Priority = tempTaskPriority,
                Status = tempTaskStatus
            };

            bool createSucces = repository.CreateTask(task);
            if (createSucces)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Task created");
                Console.ResetColor();
            }
            break;

        case ConsoleKey.D3:
            Console.Clear();

            Console.Write("What you want update? (Task, Priority, Status): ");
            string tempUpdate = Console.ReadLine()!;
            Console.Write("which one? (Id): ");
            int tempUpdateId = int.Parse(Console.ReadLine()!);
            Console.Write("Enter text: ");
            string tempUpdateText = Console.ReadLine()!;

            bool updateSucces = false;
            if (tempUpdate.Contains("Task") || tempUpdate.Contains("Priority") || tempUpdate.Contains("Status"))
            {
                if (tempUpdate.Contains("Status"))
                {
                    tempUpdate = "TaskStatuses";
                }
                if (tempUpdateId is not 0 && !string.IsNullOrEmpty(tempUpdateText))
                {
                    updateSucces = repository.UpdateTask(tempUpdate, tempUpdateId, tempUpdateText);
                }
            }

            if (updateSucces)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Task updated");
                Console.ResetColor();
            }

            break;

        case ConsoleKey.D4:
            Console.Clear();

            Console.Write("Enter Task ID: ");
            int tempTaskId = int.Parse(Console.ReadLine()!);

            bool deleteSucces = false;
            if (tempTaskId is not 0)
            {
                deleteSucces = repository.DeleteTask(tempTaskId);
            }

            if (deleteSucces)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Task deleted");
                Console.ResetColor();
            }
            break;

        case ConsoleKey.Escape:
            Environment.Exit(0);
            break;
    }
    Console.ReadLine();
    Console.Clear();
}
