using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using user.src.Database;
using user.src.DTO;
using user.src.Entity;
using static user.src.DTO.CategoryDTO;
using user.src.Utils;

namespace user.src.Repository
{
    public class ProductRepository
    {
        protected DbSet<Product> _product;
        protected DatabaseContext _databaseContext;

        public ProductRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _product = databaseContext.Set<Product>();
        }

        public async Task<Product> CreateOneAsync(Product newProduct)
        {
            await _product.AddAsync(newProduct);
            await _databaseContext.SaveChangesAsync();
            return newProduct;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _product.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            //return await _product.FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<bool> DeleteOneAsync(Product product)
        {
            _product.Remove(product);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(Product updateProduct)
        {
            _product.Update(updateProduct);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAllAsync(PaginationOptions options)
        {
            // Start with all products
            // with category information
            // var products = _product.Include(p => p.Category).ToList();

            // without category information

            var products = _product.ToList();
            // search
            if (!string.IsNullOrEmpty(options.Search))
            {
                products = products
                    .Where(p => p.Name.Contains(options.Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // min price
            if (options.MinPrice.HasValue && options.MinPrice > 0)
            {
                products = products
                    .Where(p => p.Price >= options.MinPrice)
                    .ToList();
            }
            // max price
            if (options.MinPrice.HasValue && options.MaxPrice < decimal.MaxValue)
            {
                products = products
                    .Where(p => p.Price <= options.MaxPrice)
                    .ToList();
            }

            // Apply pagination 
            products = products
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToList();

            return products;
        }

        // Count all products
        public async Task<int> CountAsync()
        {
            return await _databaseContext.Set<Product>().CountAsync();
        }

        public async Task<Product?> PatchOneAsync(Guid id, Product updatedFields)
        {
            var existingProduct = await GetByIdAsync(id);

            if (existingProduct == null)
            {
                return null; // Product not found
            }

            // Update only the fields that are not null
            if (!string.IsNullOrEmpty(updatedFields.Name))
            {
                existingProduct.Name = updatedFields.Name;
            }

            // Add other fields as needed, like price, description, etc.
            // if (updatedFields.Price != null) { existingProduct.Price = updatedFields.Price; }

            // Save the changes
            await _databaseContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}