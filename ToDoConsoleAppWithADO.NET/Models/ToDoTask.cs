namespace ToDoConsoleAppWithADO.NET.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public int ForeignKey_TaskId { get; set; } //Foreign key to task id 
        public string TaskName { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"ID: {Id}\nTask: {TaskName}\nPriority: {Priority}\nStatus: {Status}\n";
        }
    }
}
