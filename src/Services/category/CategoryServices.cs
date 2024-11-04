using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user.src.DTO;
using AutoMapper;
using static user.src.DTO.CategoryDTO;
using user.src.Entity;
using user.src.Repository;
using user.src.Utils;


namespace user.src.Services
{
    public class CategoryServices : ICategoryService
    {
        protected readonly CategoryRepo _categoryRepo;
        protected readonly IMapper _mapper;


        public CategoryServices(CategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto)
        {
            // convert CategoryCreateDto to a Category entity
            var category = _mapper.Map<CategoryCreateDto, Category>(createDto);
            // save the new category to the database
            //var categoryCreated = await _categoryRepo.CreateOneAsync(category);
            var categoryCreated = await _categoryRepo.CreateOneAsync(category);

            // convert the created Category entity to CategoryReadDto and return it.
            return _mapper.Map<Category, CategoryReadDto>(categoryCreated);
        }


        // get all
        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var categoryList = await _categoryRepo.GetAllWithPaginationAsync();
            return _mapper.Map<List<Category>, List<CategoryReadDto>>(categoryList);
        }

        // id
        public async Task<CategoryReadDtoPro> GetByIdAsync(Guid id)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(id);
            if (foundCategory == null)
            {
                throw CustomException.NotFound($"Category with {id} is not found");
            }
            return _mapper.Map<Category, CategoryReadDtoPro>(foundCategory);
        }

        // delete
        public async Task<bool> DeleteOneASync(Guid id)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(id);
            if (foundCategory is not null)
            {
                return await _categoryRepo.DeleteOneAsync(foundCategory);
            }
            return false;
        }


        // update
        public async Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(id);
            if (foundCategory == null)
            {
                return false;
            }
            _mapper.Map(updateDto, foundCategory);
            return await _categoryRepo.UpdateOneAsync(foundCategory);

        }

    }
}