using EnterpriseOrderManagement.Assembler.Assembler;
using EnterpriseOrderManagement.Assembler.IAssembler;
using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Repository.IRepository;
using EnterpriseOrderManagement.Services.IService;

namespace EnterpriseOrderManagement.Services.Service;

public sealed class CategoryService(
    ICategoryRepository categoryRepository,
    ICategoryAssembler categoryAssembler) : ICategoryService
{
    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await categoryRepository.GetAllAsync(cancellationToken);
        return categories.Select(category => categoryAssembler.ToDto(category)).ToList();
    }

    public async Task<CategoryDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        ValidateId(id);
        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);
        return category is null ? null : categoryAssembler.ToDto(category);
    }

    public async Task<CategoryDto> CreateAsync(
        CategoryDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateName(dto.Name);

        var category = categoryAssembler.ToEntity(dto);
        category.CreatedAt = DateTime.UtcNow;
        category.CreatedBy = "System";

        var createdCategory = await categoryRepository.CreateAsync(category, cancellationToken);
        return categoryAssembler.ToDto(createdCategory);
    }

    public async Task<CategoryDto?> UpdateAsync(
        int id,
        CategoryDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateId(id);
        ValidateName(dto.Name);

        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);
        if (category is null)
        {
            return null;
        }

        category.Name = dto.Name.Trim();
        category.Description = dto.Description?.Trim();
        category.IsActive = dto.IsActive;
        category.UpdatedAt = DateTime.UtcNow;
        category.UpdatedBy = "System";

        await categoryRepository.UpdateAsync(category, cancellationToken);
        return categoryAssembler.ToDto(category);
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        ValidateId(id);

        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);
        if (category is null)
        {
            return false;
        }

        await categoryRepository.DeleteAsync(category, cancellationToken);
        return true;
    }

    private static void ValidateId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Category id must be greater than zero.", nameof(id));
        }
    }

    private static void ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name is required.", nameof(name));
        }
    }
}