using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseOrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryService CategoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var category = await CategoryService.GetAllAsync(cancellationToken);
            return Ok(category);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var brand = await CategoryService.GetByIdAsync(id, cancellationToken);

            return brand is null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CategoryDto dto,
            CancellationToken cancellationToken)
        {
            var category = await CategoryService.CreateAsync(dto, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CategoryDto dto,
            CancellationToken cancellationToken)
        {
            var category = await CategoryService.UpdateAsync(id, dto, cancellationToken);

            return category is null ? NotFound() : Ok(category);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await CategoryService.DeleteAsync(id, cancellationToken);

            return isDeleted ? NoContent() : NotFound();
        }
    }
}
