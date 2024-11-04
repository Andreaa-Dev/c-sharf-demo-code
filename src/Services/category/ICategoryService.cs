using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user.src.Utils;
using static user.src.DTO.CategoryDTO;

namespace user.src.Services
{
    public interface ICategoryService
    {
       
        Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto);

        Task<List<CategoryReadDto>> GetAllAsync();

        Task<CategoryReadDtoPro> GetByIdAsync(Guid id);

        Task<bool> DeleteOneASync(Guid id);

        // update
        Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto);




    }
}