using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using user.src.Database;
using user.src.Entity;
using user.src.Utils;

namespace user.src.Repository
{
    public class CategoryRepo
    {
        protected readonly DbSet<Category> _category;
        protected readonly DatabaseContext _databaseContext;
        public CategoryRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _category = databaseContext.Set<Category>();

        }


        public async Task<Category> CreateOneAsync(Category createObject)
        {
            await _category.AddAsync(createObject);
            await _databaseContext.SaveChangesAsync();
            return createObject;
        }


        public async Task<List<Category>> GetAllWithPaginationAsync()
        {
            return await _category.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            var category = await _category.FirstOrDefaultAsync(c => c.Id == id);
            return category;

        }

        public async Task<bool> UpdateOneAsync(Category updateObject)
        {
            _category.Update(updateObject);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOneAsync(Category deleteObject)
        {
            _category.Remove(deleteObject);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // public async Task<Category> UpdateOneAsync(Category updateObject)
        // {
        //     _category.Update(updateObject); // Update the category
        //     await _databaseContext.SaveChangesAsync(); // Save changes to the database
        //     return updateObject; // Return the updated category
        // }

        // patch
        // public async Task<Category> PatchCategoryAsync(int categoryId, Category updatedProperties)
        // {
        //     // Find the existing category in the database
        //     var existingCategory = await _category.FindAsync(categoryId);
        //     if (existingCategory == null)
        //     {
        //         // Handle the case where the category does not exist
        //         throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        //     }

        //     // Update only the properties that are provided in the updatedProperties object
        //     if (updatedProperties.Name != null)
        //     {
        //         existingCategory.Name = updatedProperties.Name;
        //     }

        //     if (updatedProperties.Description != null)
        //     {
        //         existingCategory.Description = updatedProperties.Description;
        //     }

        //     // You can add more properties to update as needed

        //     // Save changes to the database
        //     await _databaseContext.SaveChangesAsync();

        //     // Return the updated category
        //     return existingCategory;
        // }

    }
}