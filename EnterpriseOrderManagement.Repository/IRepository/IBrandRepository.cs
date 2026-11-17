using EnterpriseOrderManagement.Domain.Model;

namespace EnterpriseOrderManagement.Repository.IRepository;

public interface IBrandRepository
{
    Task<List<Brand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Brand> CreateAsync(Brand brand, CancellationToken cancellationToken = default);
    Task UpdateAsync(Brand brand, CancellationToken cancellationToken = default);
    Task DeleteAsync(Brand brand, CancellationToken cancellationToken = default);
}
