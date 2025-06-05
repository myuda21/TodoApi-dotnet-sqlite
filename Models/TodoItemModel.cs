using System.Text.Json.Serialization;

namespace TODOAPI.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsCompleted { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; } // relasi ke user
    }
}