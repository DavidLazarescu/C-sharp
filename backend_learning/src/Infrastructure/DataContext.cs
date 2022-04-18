using System.Reflection;
using backend_learning.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace backend_learning.Infrastructure;

// The class inheriting "DbContext" is the connection between the application and the Db. It defines DbSet<>'s which
// are basically tables. You can use operations like "Add", "Remove" or LINQ queries on it, to work with the Db.
public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }  // This is the table of type User called "Users" in the databank, it manages all users
    public DbSet<Job> Jobs { get; set; }  // This is the table of type User called "Jobs" in the databank


    public DataContext() : base()
    {
    }

    // This constructor will most of the time stay the same when working with ASP.NET, you just call the base object's constructor
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    // In this method, you write all your Entity configurations. Here, all of these are extracted into seperate classes
    // which are then applied by the method "ApplyConfiguration"
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        // This method searches the specified assembly, looking for classes implementing "IEntityTypeConfiguration" and then applying it.
        // This is clean, because you don't need to add each class manually, in case you would want to add them manually, use:
        // "modelBuilder.ApplyConfiguration(new MyConfiguration());"
        modelBuilder.ApplyConfigurationsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == "Domain"));
    }
}