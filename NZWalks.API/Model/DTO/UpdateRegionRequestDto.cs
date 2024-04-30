using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Model.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be of minimum 3 characters")]
        [MaxLength(3, ErrorMessage = "Code must be of maximum 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name must be of maximum 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
