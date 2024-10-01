using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using user.src.Services.product;
using static user.src.DTO.ProductDTO;

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

        public async Task<ActionResult<List<ProductReadDto>>> GetAllAsync()
        {
            var productList = await _productService.GetAllAsync();
            return Ok(productList);
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
        public async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            var isDeleted = await _productService.DeleteOneASync(id);
            System.Console.WriteLine(isDeleted);
            return Ok(isDeleted);
        }
    }
}