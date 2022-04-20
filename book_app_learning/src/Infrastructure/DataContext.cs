using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DataContext : DbContext
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Book> Books { get; set; }


    public DataContext() : base()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}