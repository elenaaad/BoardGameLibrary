using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Dtos
{
    public class BoardGameCreateDtos
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int NumberOfPlayers { get; set; } = 0;
        [Required]

        public string Language { get; set; } = string.Empty;

    }
}