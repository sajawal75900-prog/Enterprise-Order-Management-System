using EnterpriseOrderManagement.Assembler;
using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Repository.IRepository;
using EnterpriseOrderManagement.Services.IService;

namespace EnterpriseOrderManagement.Services.Service;

public sealed class BrandService(IBrandRepository brandRepository) : IBrandService
{
    public async Task<IReadOnlyList<BrandDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var brands = await brandRepository.GetAllAsync(cancellationToken);
        return brands.Select(brand => brand.ToDto()).ToList();
    }

    public async Task<BrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        ValidateId(id);
        return (await brandRepository.GetByIdAsync(id, cancellationToken))?.ToDto();
    }

    public async Task<BrandDto> CreateAsync(BrandDto dto, CancellationToken cancellationToken = default)
    {
        ValidateName(dto.Name);
        var brand = await brandRepository.CreateAsync(dto.ToEntity(), cancellationToken);
        return brand.ToDto();
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

        brand.ApplyUpdate(dto);
        await brandRepository.UpdateAsync(brand, cancellationToken);
        return brand.ToDto();
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
