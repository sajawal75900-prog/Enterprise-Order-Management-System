using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseOrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController(IBrandService brandService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var brands = await brandService.GetAllAsync(cancellationToken);
        return Ok(brands);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var brand = await brandService.GetByIdAsync(id, cancellationToken);

        return brand is null ? NotFound() : Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] BrandDto dto,
        CancellationToken cancellationToken)
    {
        var brand = await brandService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = brand.Id },
            brand);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] BrandDto dto,
        CancellationToken cancellationToken)
    {
        var brand = await brandService.UpdateAsync(id, dto, cancellationToken);

        return brand is null ? NotFound() : Ok(brand);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var isDeleted = await brandService.DeleteAsync(id, cancellationToken);

        return isDeleted ? NoContent() : NotFound();
    }
}
