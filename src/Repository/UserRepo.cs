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
    public class UserRepo
    {
        protected readonly DbSet<User> _user;
        protected readonly DatabaseContext _databaseContext;

        public UserRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _user = databaseContext.Set<User>();
        }

        // register - only need this
        public async Task<User> CreateOneAsync(User createObject)
        {
            await _user.AddAsync(createObject);
            await _databaseContext.SaveChangesAsync();
            return createObject;
        }


        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _user.AnyAsync(u => u.Email == email);
        }


        public async Task<bool> DeleteOneAsync(User deleteObject)
        {
            _user.Remove(deleteObject);
            await _databaseContext.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<User>> GetAllAsync(PaginationOptions getAllOptions)
        {
            return await _user.Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
        }


        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _user.FindAsync(id);
        }

        public async Task<User> UpdateOneAsync(User updateObject)
        {
            _user.Update(updateObject);
            await _databaseContext.SaveChangesAsync();
            return updateObject;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _user.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}