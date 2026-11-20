using EnterpriseOrderManagement.Domain;
using EnterpriseOrderManagement.Domain.Model;
using EnterpriseOrderManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseOrderManagement.Repository.Repository
{
    public sealed class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
    {
        public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default) =>
            context.Categories
                .AsNoTracking()
                .OrderBy(category => category.Name)
                .ToListAsync(cancellationToken);

        public Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
            context.Categories.FirstOrDefaultAsync(category => category.Id == id, cancellationToken);

        public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync(cancellationToken);
            return category;
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
