using System.ComponentModel.DataAnnotations;

namespace NZworks.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Code must be at least 2 characters long.")]
        [MaxLength(5, ErrorMessage = "Code cannot be more than 5 characters long.")]
        public string Code { get; set; }

        [Required]
        [MinLength(length: 2, ErrorMessage = "Name must have at least be 2 characters long!!!")]
        [MaxLength(100, ErrorMessage = "Name cannot be more than 100 characters long.")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }

    }
}
