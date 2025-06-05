namespace TODOAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
