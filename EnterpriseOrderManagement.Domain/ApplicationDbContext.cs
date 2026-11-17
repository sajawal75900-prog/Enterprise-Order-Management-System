using EnterpriseOrderManagement.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseOrderManagement.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Brand> Brands => Set<Brand>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
