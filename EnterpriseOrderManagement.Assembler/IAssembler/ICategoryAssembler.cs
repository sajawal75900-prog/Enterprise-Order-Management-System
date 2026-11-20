using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Domain.Model;

namespace EnterpriseOrderManagement.Assembler.IAssembler
{
    public interface ICategoryAssembler
    {
        // Entity to DTO
        CategoryDto ToDto(Category category);

        // DTO to Entity
        Category ToEntity(CategoryDto dto);
    }
}
