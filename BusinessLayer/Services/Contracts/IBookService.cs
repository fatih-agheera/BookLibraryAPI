using BusinessLayer.Models.Book;
using DataLayer.Models;

namespace BusinessLayer.Services.Contracts
{
	public interface IBookService
	{
		IEnumerable<Book> GetAll();

		Book GetById(string id);

		Book GetByIsbn(string isbn);

		Book Create(CreateBookDto item);

		Book Update(string id, UpdateBookDto updateBookDto);

		Book Delete(string id);
	}
}
