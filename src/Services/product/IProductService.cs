using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static user.src.DTO.ProductDTO;
using user.src.Utils;

namespace user.src.Services.product
{
    public interface IProductService
    {
        Task<ProductReadDto> CreateOneAsync(ProductCreateDto createDto);
        Task<bool> DeleteOneASync(Guid id);
        Task<List<ProductReadDto>> GetAllAsync(PaginationOptions options);
        Task<int> CountProductsAsync(); 
        Task<ProductReadDto> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, ProductUpdateDto updateDto);
    }
}