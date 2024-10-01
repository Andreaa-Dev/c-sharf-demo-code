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
        // define the method
        // create
        Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto);

        // get all
        Task<List<CategoryReadDto>> GetAllAsync(PaginationOptions paginationOptions);
        //Task<IEnumerable<CategoryReadDto>> GetAllAsync(GetAllOptions getAllOptions);

        // get id
        Task<CategoryReadDtoPro> GetByIdAsync(Guid id);

        // delete
        Task<bool> DeleteOneASync(Guid id);

        // update
        Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto);




    }
}