using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Domain.Model;

namespace EnterpriseOrderManagement.Assembler;

public static class BrandAssembler
{
    public static Brand ToEntity(this BrandDto dto) => new()
    {
        Name = dto.Name.Trim(),
        Description = dto.Description?.Trim(),
        IsActive = dto.IsActive
    };

    public static void ApplyUpdate(this Brand brand, BrandDto dto)
    {
        brand.Name = dto.Name.Trim();
        brand.Description = dto.Description?.Trim();
        brand.IsActive = dto.IsActive;
    }

    public static BrandDto ToDto(this Brand brand) => new()
    {
        Id = brand.Id,
        Name = brand.Name,
        Description = brand.Description,
        IsActive = brand.IsActive
    };
}
