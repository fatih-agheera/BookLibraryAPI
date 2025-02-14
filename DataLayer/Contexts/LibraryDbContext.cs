using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace DataLayer.Contexts;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options): base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToCollection("Users");
        modelBuilder.Entity<Book>().ToCollection("Books");
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
}