namespace MicroServiceSetup.Models
{
    public class Todo
    {
        public int Id { get; set; } // auto increment --> handled via DB
        public string Name { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}