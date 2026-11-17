using System.ComponentModel.DataAnnotations;

namespace EnterpriseOrderManagement.Domain.Dtos;

public class BrandDto
{
    public int Id { get; set; }

    [Required, StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}
