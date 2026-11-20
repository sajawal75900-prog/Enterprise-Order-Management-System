using System;
using System.Collections.Generic;
using System.Text;
using EnterpriseOrderManagement.Domain.Dtos;
using EnterpriseOrderManagement.Domain.Model;

namespace EnterpriseOrderManagement.Assembler.IAssembler
{
    public interface IBrandAssembler
    {
        // Entity to DTO
        BrandDto ToDto(Brand brand);

        // DTO to Entity
        Brand ToEntity(BrandDto dto);
    }
}
