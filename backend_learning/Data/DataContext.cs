using System.Reflection;
using backend_learning.Entities;
using backend_learning.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace backend_learning.Data
{
    // The class inheriting "DbContext" is the connection between the application and the Db. It defines DbSet<>'s which
    // are basically tables. You can use operations like "Add", "Remove" or LINQ queries on it, to work with the Db.
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }  // This is the table of type User called "Users" in the databank, it manages all users

        // This constructor will most of the time stay the same when working with ASP.NET, you just call the base object's constructor
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // In this method, you write all your Entity configurations. Here, all of these are extracted into seperate classes
        // which are then applied by the method "ApplyConfiguration"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
            // This method searches the specified assembly, looking for classes implementing "IEntityTypeConfiguration" and then applying it.
            // This is clean, because you don't need to add each class manually, in case you would want to add them manually, use:
            // "modelBuilder.ApplyConfiguration(new MyConfiguration());"
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}