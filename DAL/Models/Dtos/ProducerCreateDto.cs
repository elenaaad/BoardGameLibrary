using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Dtos
{
    public class ProducerCreateDtos
    {
        [MaxLength(30)]
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}