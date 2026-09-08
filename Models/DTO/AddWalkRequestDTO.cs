using System.ComponentModel.DataAnnotations;
using NZworks.Models.Domain;

namespace NZworks.Models.DTO
{
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is Guid guidValue)
            {
                return guidValue != Guid.Empty;
            }

            return false;
        }
    }

    public class AddWalkRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "LengthInKm must be greater than 0.")]

        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        [Required(ErrorMessage = "Region Id is required")]
        [NotEmptyGuid(ErrorMessage = "Region Id cannot be empty")]

        public Guid RegionId { get; set; }

        [Required(ErrorMessage = "Difficulty Id is required")]
        [NotEmptyGuid(ErrorMessage = "Difficulty Id cannot be empty")]
        public Guid DifficultyId { get; set; }

        // Navigation properties
        //public Region Region { get; set; }
        //public Difficulty Difficulty { get; set; }
    }
}
