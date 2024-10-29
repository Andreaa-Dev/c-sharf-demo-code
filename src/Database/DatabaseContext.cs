using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using user.src.Entity;

namespace user.src.Database
{
    // which serves as the bridge between your application and the database.
    public class DatabaseContext : DbContext
    {
        // step 2: table
        // name of table: Category
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Order> Order { get; set; }



        // step 3: constructor 
        // no need logic
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        // for role
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // enum => save as number 
            modelBuilder.HasPostgresEnum<Role>();
            // modelBuilder.Entity<Category>().Navigation(s => s.Products).AutoInclude();
            // Add unique constraint to User.Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }


}