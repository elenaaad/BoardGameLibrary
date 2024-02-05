using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Dtos
{
    public class BoardGameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int NumberOfPlayers { get; set; }

        public string Language { get; set; }
        public string DurationOfPlay { get; set; }

        public Guid ProducerId { get; set; }

        public string ProducerName { get; set; }

    }
}