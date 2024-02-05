using Demo.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Producer : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public ICollection<BoardGame?> BoardGames { get; set; } = new List<BoardGame>();

    }
}