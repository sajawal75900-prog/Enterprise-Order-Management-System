using EnterpriseOrderManagement.Domain.Dtos;

namespace EnterpriseOrderManagement.Services.IService;

public interface IBrandService
{
    Task<IReadOnlyList<BrandDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<BrandDto> CreateAsync(BrandDto dto, CancellationToken cancellationToken = default);
    Task<BrandDto?> UpdateAsync(int id, BrandDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
