using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
	public class LibraryDbContext : DbContext
	{
		public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

		public DbSet<Book> Books { get; set; }

		public DbSet<User> Users { get; set; }
	}
}
