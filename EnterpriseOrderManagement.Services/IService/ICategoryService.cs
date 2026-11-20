using System;
using System.Collections.Generic;
using System.Text;
using EnterpriseOrderManagement.Domain.Dtos;

namespace EnterpriseOrderManagement.Services.IService
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<CategoryDto> CreateAsync(CategoryDto dto, CancellationToken cancellationToken = default);
        Task<CategoryDto?> UpdateAsync(int id, CategoryDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
