namespace ToDoConsoleAppWithADO.NET.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public int ForeignKey_TaskId { get; set; } //Foreign key to task id 
        public string TaskName { get; set; } 
        public string Priority { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}\nTask: {TaskName}\nPriority: {Priority}\nStatus: {Status}\n";
        }
    }
}
