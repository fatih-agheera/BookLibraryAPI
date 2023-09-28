using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories.Implementations
{
	public class BookRepository : BaseRepository<Book>, IBookRepository
	{
		public BookRepository(LibraryDbContext context) : base(context) { }

		public virtual Book? GetByIsbn(string isbn)
			=> _dbSet.FirstOrDefault(x => x.Isbn == isbn);

		public bool IsUniqueIsbn(string isbn)
		{
			var result = _dbSet.Any(x => x.Isbn == isbn);
			return !result;
		}
	}
}
