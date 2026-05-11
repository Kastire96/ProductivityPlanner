using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductivityPlanner.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<TodoTask> Tasks { get; set; } = new List<TodoTask>();
    }
}
