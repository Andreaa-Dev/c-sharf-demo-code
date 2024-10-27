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

        // Create a new product
        public async Task<Product> CreateOneAsync(Product newProduct)
        {
            await _product.AddAsync(newProduct);
            await _databaseContext.SaveChangesAsync();
            return newProduct;
        }

        // Get a product by Id
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _product.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            //return await _product.FirstOrDefaultAsync(p => p.Id == id);

        }

        // Delete a product
        public async Task<bool> DeleteOneAsync(Product product)
        {
            _product.Remove(product);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // Update a product
        public async Task<bool> UpdateOneAsync(Product updateProduct)
        {
            _product.Update(updateProduct);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // Get all products (optional: add pagination)
        public async Task<List<Product>> GetAllAsync(PaginationOptions options)
        {
            // return await _product.ToListAsync();
            // return await _product.Include(p => p.Category).ToListAsync();
            // return await _product.Skip((pageNumber - 1) * pageSize)
            //                      .Take(pageSize)
            //                      .ToListAsync();

            // Start with all products
            // var products = _product.Include(p => p.Category).ToList();
            var products = _product.ToList();

            if (!string.IsNullOrEmpty(options.Search))
            {
                products = products
                    .Where(p => p.Name.Contains(options.Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (options.MinPrice.HasValue && options.MinPrice > 0 )
            {
                products = products
                    .Where(p => p.Price >= options.MinPrice)
                    .ToList();
            }

            if (options.MinPrice.HasValue && options.MaxPrice < decimal.MaxValue)
            {
                products = products
                    .Where(p => p.Price <= options.MaxPrice)
                    .ToList();
            }

            // Apply pagination in-memory
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

        // Partial update (PATCH)
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