using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using user.src.Services.product;
using static user.src.DTO.ProductDTO;

using user.src.Utils;
using Microsoft.AspNetCore.Authorization;

namespace user.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        protected readonly IProductService _productService;

        public ProductsController(IProductService service)
        {
            _productService = service;
        }
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> CreateOneAsync([FromBody] ProductCreateDto createDto)
        {
            var productCreated = await _productService.CreateOneAsync(createDto);
            return Ok(productCreated);
        }


        [HttpGet]
        // old"Task<ActionResult<List<ProductReadDto>>>
        public async Task<ActionResult<List<ProductListDto>>> GetAllAsync([FromQuery] PaginationOptions options)
        {
            var productList = await _productService.GetAllAsync(options);
            // calculate the total count 
            var totalCount = await _productService.CountProductsAsync();

            var response = new ProductListDto
            {
                Products = productList,
                TotalCount = totalCount
            };

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductReadDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, ProductUpdateDto updateDto)
        {
            var isUpdated = await _productService.UpdateOneAsync(id, updateDto);
            return Ok(isUpdated);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            var isDeleted = await _productService.DeleteOneASync(id);
            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}