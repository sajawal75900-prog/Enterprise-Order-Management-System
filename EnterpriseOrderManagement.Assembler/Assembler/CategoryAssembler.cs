using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Domain.Model;
using EnterpriseOrderManagement.Assembler.IAssembler;

namespace EnterpriseOrderManagement.Assembler.Assembler;

public sealed class CategoryAssembler : ICategoryAssembler
{
    // Entity to DTO
    public CategoryDto ToDto(Category category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description,
        IsActive = category.IsActive,
        CreatedAt = category.CreatedAt,
        CreatedBy = category.CreatedBy,
        UpdatedAt = category.UpdatedAt,
        UpdatedBy = category.UpdatedBy
    };

    // DTO to Entity
    public Category ToEntity(CategoryDto dto) => new()
    {
        Name = dto.Name.Trim(),
        Description = dto.Description?.Trim(),
        IsActive = dto.IsActive
    };
}