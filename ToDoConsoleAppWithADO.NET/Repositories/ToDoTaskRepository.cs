using Microsoft.Data.SqlClient;
using ToDoConsoleAppWithADO.NET.Models;

public class ToDoTaskRepository
{
    private readonly SqlConnection connection;
    private List<ToDoTask> tasks;
    private int lastTaskId;

    public ToDoTaskRepository(string connectionString)
    {
        connection = new SqlConnection(connectionString);
        connection.Open();
        tasks = new List<ToDoTask>();
    }

    private void GetLastID()
    {
        var commandText = $"Select MAX(Id) from Tasks";

        using (SqlCommand command = new SqlCommand(commandText, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    lastTaskId = reader.GetInt32(0);
                }
                lastTaskId++;
            }
        }
    }


    public IEnumerable<ToDoTask> ReadTasks()
    {
        var commandText = $@"Select t.[Id], p.[TaskId], t.[Name], p.[Name], ts.[Name]   
from Tasks t
Join Priorities p on t.[Id] = p.[TaskId]
Join TaskStatuses ts on t.[Id] = ts.[TaskId]";

        using (SqlCommand command = new SqlCommand(commandText, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var task = new ToDoTask()
                    {
                        Id = reader.GetInt32(0),
                        ForeignKey_TaskId = reader.GetInt32(1),
                        TaskName = reader.GetString(2),
                        Priority = reader.GetString(3),
                        Status = reader.GetString(4)
                    };

                    tasks.Add(task);
                }
            }
        }

        return tasks;
    }

    public bool CreateTask(ToDoTask newTask)
    {
        var insertTasksCommandText = $"Insert into Tasks([Name]) Values('{newTask.TaskName}')";
        GetLastID();
        var insertPrioritiesCommandText = $"Insert into Priorities([Name], [TaskId]) Values('{newTask.Priority}', {lastTaskId})";
        var insertTaskStatusesCommandText = $"Insert into TaskStatuses([Name], [TaskId]) Values('{newTask.Status}', {lastTaskId})";

        bool succes = false;

        using (SqlCommand command = new SqlCommand(insertTasksCommandText, connection))
        {
            if (command.ExecuteNonQuery() > 0)
                succes = true;
        }

        if (succes)
        {
            using (SqlCommand command = new SqlCommand(insertPrioritiesCommandText, connection))
            {
                if (command.ExecuteNonQuery() == -1)
                    succes = false;
            }

            if (succes)
            {
                using (SqlCommand command = new SqlCommand(insertTaskStatusesCommandText, connection))
                {
                    if (command.ExecuteNonQuery() == -1)
                        succes = false;
                }
            }
        }

        return succes;
    }

    public bool UpdateTask(string tempUpdate, int tempUpdateId, string tempUpdateText)
    {
        var commantText = $"Update {tempUpdate} Set [Name] = '{tempUpdateText}' Where [Id] = {tempUpdateId}";
        using (var command = new SqlCommand(commantText, connection))
            return command.ExecuteNonQuery() > 0;
    }

    public bool DeleteTask(int id)
    {
        var DeleteTaskStatusesCommandText = $"Delete from TaskStatuses Where [TaskId] = ({id})";
        var DeletePrioritiesCommandText = $"Delete from Priorities Where [TaskId] = ({id})";
        var DeleteTasksCommandText = $"Delete from Tasks Where [Id] = ({id})";

        bool succes = false;
        using (SqlCommand command = new SqlCommand(DeleteTaskStatusesCommandText, connection))
        {
            if (command.ExecuteNonQuery() > 0)
                succes = true;
        }

        if (succes)
        {
            using (SqlCommand command = new SqlCommand(DeletePrioritiesCommandText, connection))
            {
                if (command.ExecuteNonQuery() == -1)
                    succes = false;
            }

            if (succes)
            {
                using (SqlCommand command = new SqlCommand(DeleteTasksCommandText, connection))
                {
                    if (command.ExecuteNonQuery() == -1)
                        succes = false;
                }
            }
        }

        return succes;
    }
}


