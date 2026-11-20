using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Domain.Model;

namespace EnterpriseOrderManagement.Assembler.Assembler;

public static class BrandAssembler
{
    // Entity to DTO
    public static BrandDto ToDto(this Brand brand) => new()
    {
        Id = brand.Id,
        Name = brand.Name,
        Description = brand.Description,
        IsActive = brand.IsActive,
        CreatedAt = brand.CreatedAt,
        CreatedBy = brand.CreatedBy,
        UpdatedAt = brand.UpdatedAt,
        UpdatedBy = brand.UpdatedBy
    };

    // DTO to Entity
    public static Brand ToEntity(this BrandDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name.Trim(),
        Description = dto.Description?.Trim(),
        IsActive = dto.IsActive,
        CreatedAt = dto.CreatedAt,
        CreatedBy = dto.CreatedBy,
        UpdatedAt = dto.UpdatedAt,
        UpdatedBy = dto.UpdatedBy
    };
}