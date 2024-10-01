using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using user.src.Database;
using user.src.DTO;
using user.src.Entity;
using static user.src.DTO.CategoryDTO;

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

        // public async Task<Product> CreateOneAsync(Product newProduct, Guid categoryId)
        // {
        //     // Set the CategoryId (or fetch and set the Category entity as in option 1)
        //     newProduct.CategoryId = categoryId;

        //     // Add the product to the database
        //     await _product.AddAsync(newProduct);
        //     await _databaseContext.SaveChangesAsync();

        //     // Optionally, fetch the product again to include the category
        //     return await GetByIdAsync(newProduct.Id);
        // }

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
        public async Task<List<Product>> GetAllAsync()
        {
            return await _product.ToListAsync();
            // return await _product.Include(p => p.Category).ToListAsync();
            // return await _product.Skip((pageNumber - 1) * pageSize)
            //                      .Take(pageSize)
            //                      .ToListAsync();
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