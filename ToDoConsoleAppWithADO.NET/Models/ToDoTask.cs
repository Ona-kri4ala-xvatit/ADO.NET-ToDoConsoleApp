namespace ToDoConsoleAppWithADO.NET.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}\nTask: {Name}\n";
        }
    }
}
