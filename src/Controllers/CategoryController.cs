using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user.src.Services;
using user.src.Utils;
using static user.src.DTO.CategoryDTO;

namespace user.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        protected readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }


        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> CreateOneController([FromBody] CategoryCreateDto createDto)
        {
            var categoryCreated = await _categoryService.CreateOneAsync(createDto);
            //return Ok(categoryCreated);
            return Created($"api/v1/category/{categoryCreated.Id}", categoryCreated);
        }


        [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetAllAsync()
        {
            var categoryList = await _categoryService.GetAllAsync();
            return Ok(categoryList);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryReadDtoPro>> GetByIdAsync([FromRoute] Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            var isDeleted = await _categoryService.DeleteOneASync(id);
            return Ok(isDeleted);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryReadDto>> CreateOneAsync([FromRoute] Guid id, [FromBody] CategoryUpdateDto updateDto)
        {
            var categoryUpdated = await _categoryService.UpdateOneAsync(id, updateDto);
            return Ok(categoryUpdated);

        }
    }
}