using EnterpriseOrderManagement.Assembler.IAssembler;
using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Repository.IRepository;
using EnterpriseOrderManagement.Services.IService;

namespace EnterpriseOrderManagement.Services.Service;

public sealed class BrandService(IBrandRepository brandRepository,IBrandAssembler brandAssembler) : IBrandService
{
    public async Task<IReadOnlyList<BrandDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var brands = await brandRepository.GetAllAsync(cancellationToken);

        return brands
            .Select(brand => brandAssembler.ToDto(brand))
            .ToList();
    }

    public async Task<BrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        ValidateId(id);

        var brand = await brandRepository.GetByIdAsync(id, cancellationToken);

        return brand is null ? null : brandAssembler.ToDto(brand);
    }

    public async Task<BrandDto> CreateAsync(BrandDto dto, CancellationToken cancellationToken = default)
    {
        ValidateName(dto.Name);

        var brand = await brandRepository.CreateAsync(brandAssembler.ToEntity(dto), cancellationToken);

        return brandAssembler.ToDto(brand);
    }

    public async Task<BrandDto?> UpdateAsync(int id, BrandDto dto, CancellationToken cancellationToken = default)
    {
        ValidateId(id);
        ValidateName(dto.Name);

        var brand = await brandRepository.GetByIdAsync(id, cancellationToken);

        if (brand is null)
        {
            return null;
        }

        // Update existing entity
        brand.Name = dto.Name.Trim();
        brand.Description = dto.Description?.Trim();
        brand.IsActive = dto.IsActive;

        // Update audit fields
        brand.UpdatedAt = DateTime.UtcNow;
        brand.UpdatedBy = "System";

        await brandRepository.UpdateAsync(brand, cancellationToken);

        return brandAssembler.ToDto(brand);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        ValidateId(id);

        var brand = await brandRepository.GetByIdAsync(id, cancellationToken);

        if (brand is null)
        {
            return false;
        }

        await brandRepository.DeleteAsync(brand, cancellationToken);

        return true;
    }

    private static void ValidateId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Brand id must be greater than zero.", nameof(id));
        }
    }

    private static void ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Brand name is required.", nameof(name));
        }
    }
}