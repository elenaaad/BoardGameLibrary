using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Dtos
{
    public class ProducerCreateDto
    {
        [MaxLength(30)]
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}