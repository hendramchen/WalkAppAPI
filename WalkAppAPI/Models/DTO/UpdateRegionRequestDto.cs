using System.ComponentModel.DataAnnotations;

namespace WalkAppAPI.Models.DTO;

public class UpdateRegionRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Code must be at least 3 characters long")]
    [MaxLength(3, ErrorMessage = "Code must be at least 3 characters long")]
    public string Code { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
    public string Name { get; set; }
    public string? RegionImageUrl { get; set; }
}