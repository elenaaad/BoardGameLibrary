
using DAL.Models.Dtos;
using Demo.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class BoardGame : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int NumberOfPlayers { get; set; } = 0;
        public string Language { get; set; } = string.Empty;
        public string DurationOfPlay { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public Guid ProducerId { get; set; }

        public Producer Producer { get; set; }
        public ICollection<BoardGameInCollection> BoardGameInCollection { get; set; } = new List<BoardGameInCollection>();

    }
}