using EnterpriseOrderManagement.Domain;
using EnterpriseOrderManagement.Domain.Model;
using EnterpriseOrderManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseOrderManagement.Repository.Repository;

public sealed class BrandRepository(ApplicationDbContext context) : IBrandRepository
{
    public Task<List<Brand>> GetAllAsync(CancellationToken cancellationToken = default) =>
        context.Brands.AsNoTracking().OrderBy(brand => brand.Name).ToListAsync(cancellationToken);

    public Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        context.Brands.FirstOrDefaultAsync(brand => brand.Id == id, cancellationToken);

    public async Task<Brand> CreateAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        context.Brands.Add(brand);
        await context.SaveChangesAsync(cancellationToken);
        return brand;
    }

    public async Task UpdateAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        context.Brands.Update(brand);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        context.Brands.Remove(brand);
        await context.SaveChangesAsync(cancellationToken);
    }
}
