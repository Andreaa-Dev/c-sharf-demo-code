using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static user.src.DTO.ProductDTO;

namespace user.src.Services.product
{
    public interface IProductService
    {
        Task<ProductReadDto> CreateOneAsync(ProductCreateDto createDto);
        Task<bool> DeleteOneASync(Guid id);
        Task<List<ProductReadDto>> GetAllAsync();
        Task<ProductReadDto> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, ProductUpdateDto updateDto);
    }
}