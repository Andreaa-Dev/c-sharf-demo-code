using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using user.src.Entity;
using user.src.Repository;
using user.src.Utils;
using static user.src.DTO.ProductDTO;

namespace user.src.Services.product
{
    public class ProductService : IProductService
    {
        protected readonly ProductRepository _productRepo;
        protected readonly IMapper _mapper;

        public ProductService(ProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ProductReadDto> CreateOneAsync(ProductCreateDto createDto)
        {
            var product = _mapper.Map<ProductCreateDto, Product>(createDto);
            var productCreated = await _productRepo.CreateOneAsync(product);
            return _mapper.Map<Product, ProductReadDto>(productCreated);
        }

        public async Task<bool> DeleteOneASync(Guid id)
        {
            var foundProduct = await _productRepo.GetByIdAsync(id);
            if (foundProduct is not null)
            {
                return await _productRepo.DeleteOneAsync(foundProduct);
            }
            return false;
        }

        public async Task<List<ProductReadDto>> GetAllAsync()
        {
            var productList = await _productRepo.GetAllAsync();
            return _mapper.Map<List<Product>, List<ProductReadDto>>(productList);
        }

        public async Task<ProductReadDto> GetByIdAsync(Guid id)
        {
            var foundProduct = await _productRepo.GetByIdAsync(id);
            if (foundProduct == null)
            {
                throw CustomException.NotFound($"Product with {id} is not found");
            }
            return _mapper.Map<Product, ProductReadDto>(foundProduct);
        }

        public async Task<bool> UpdateOneAsync(Guid id, ProductUpdateDto updateDto)
        {
            var foundProduct = await _productRepo.GetByIdAsync(id);
            if (foundProduct == null)
            {
                return false;
            }
            _mapper.Map(updateDto, foundProduct);
            return await _productRepo.UpdateOneAsync(foundProduct);

        }

    }
}