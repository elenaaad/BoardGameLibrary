using Demo.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Collection : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Description { get; set; } = string.Empty;
        public User User { get; set; }
        public Guid UserId { get; set; }
        public ICollection<BoardGameInCollection> BoardGameInCollections { get; set; } = new List<BoardGameInCollection>();

    }
}
