using Demo.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class User : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(30)]

        public string Email { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; } = string.Empty;
        public ICollection<Collection> Collections { get; set; } = new List<Collection>();
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;

        public Role Role { get; set; }
    }
}