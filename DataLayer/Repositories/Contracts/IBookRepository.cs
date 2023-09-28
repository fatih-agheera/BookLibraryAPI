using DataLayer.Models;

namespace DataLayer.Repositories.Contracts
{
	public interface IBookRepository : IBaseRepository<Book>
	{
		Book? GetByIsbn(string isbn);

		bool IsUniqueIsbn(string isbn);
	}
}
