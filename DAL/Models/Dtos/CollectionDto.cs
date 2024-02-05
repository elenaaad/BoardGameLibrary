using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Dtos
{
    public class CollectionDtos
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

        public int numberOfBoardGames { get; set; }
    }
}