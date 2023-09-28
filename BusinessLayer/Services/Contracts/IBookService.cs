using BusinessLayer.Models.Book;
using DataLayer.Models;

namespace BusinessLayer.Services.Contracts
{
	public interface IBookService
	{
		IEnumerable<Book> GetAll();

		Book GetById(int id);

		Book GetByIsbn(string isbn);

		Book Create(CreateBookDto item);

		Book Update(int id, UpdateBookDto updateBookDto);

		Book Delete(int id);
	}
}
