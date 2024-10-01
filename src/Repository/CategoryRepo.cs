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

    // logic to talk to database
    public class CategoryRepo
    {
        // This represents a collection of Category entities in the database
        // which EF Core uses to track and manipulate data. 
        // It allows querying and saving instances of Category.
        protected readonly DbSet<Category> _category;
        protected readonly DatabaseContext _databaseContext;

        // DI
        public CategoryRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            // initializes _category to the database
            _category = databaseContext.Set<Category>();

        }

        public Category CreateOne(Category createObject)
        {

            _category.Add(createObject); // Synchronous Add
            _databaseContext.SaveChanges(); // Synchronous SaveChanges
            return createObject;
        }

        // Non-blocking; allows other requests to be processed while waiting for the database response

        public async Task<Category> CreateOneAsync(Category createObject)
        {
            await _category.AddAsync(createObject);
            await _databaseContext.SaveChangesAsync();
            return createObject;
        }



        // list
        // IEnumerable is better
        // returning a List<Category> could consume more memory compared to returning IEnumerable<Category>
        // Returning IEnumerable<Category> allows for more flexibility in terms of how the data is consumed
        // Consumers of the method can choose to convert it to a different collection type if needed.
        // for example to List or Array
        // public async Task<List<Category>> GetAllAsync()
        // {
        //     return await _category.ToListAsync();
        //     // return await _category.Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
        // }


        public async Task<List<Category>> GetAllWithPaginationAsync(PaginationOptions paginationOptions)
        {
            // http://localhost:5000/api/v1/categorys?offset=0&limit=10&search=c
            // c.Name.Equals(searchTerm, StringComparison.OrdinalIgnoreCase)
            // Name.ToLower()
            var result = _category.Where(c => c.Name.Contains(paginationOptions.Search));
            return await result.Skip(paginationOptions.Offset).Take(paginationOptions.Limit).ToListAsync();
            //return await _category.Skip(paginationOptions.Offset).Take(paginationOptions.Limit).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            //return await _category.FindAsync(id);
            var category = await _category.FirstOrDefaultAsync(c => c.Id == id);

            // if (category != null)
            // {
            //     // Explicitly load the related Products collection
            //     await _databaseContext.Entry(category)
            //                           .Collection(c => c.Products)
            //                           .LoadAsync();
            // }

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