using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.Book;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations
{
	public class BookService : IBookService
	{
		private readonly IRepositoriesWrapper _repositoriesWrapper;

		private readonly IMapper _mapper;

		public BookService(IRepositoriesWrapper repositoriesWrapper, IMapper mapper)
		{
			_repositoriesWrapper = repositoriesWrapper;
			_mapper = mapper;
		}


		public Book GetById(int id)
		{
			var book = _repositoriesWrapper.Books.Get(id);
			if (book == null)
			{
				throw new DbEntityNotFoundException($"Book with id {id} not found int the database.");
			}

			return book;
		}
		public IEnumerable<Book> GetAll()
			=> _repositoriesWrapper.Books.GetAll();

		public Book GetByIsbn(string isbn)
		{
			var book = _repositoriesWrapper.Books.GetByIsbn(isbn);
			if (book == null)
			{
				throw new DbEntityNotFoundException($"Book with ISBN {isbn} not found int the database.");
			}

			return book;
		}

		public Book Create(CreateBookDto createBookDto)
		{
			var isUniqueIsbn = _repositoriesWrapper.Books.IsUniqueIsbn(createBookDto.Isbn);

			if (!isUniqueIsbn)
			{
				throw new ItemAlreadyExistsException($"ISBN: '{createBookDto.Isbn}' already exists in database!");
			}

			var book = _mapper.Map<Book>(createBookDto);
			_repositoriesWrapper.Books.Save(book);
			return book;
		}

		public Book Update(int id, UpdateBookDto updateBookDto)
		{
			var bookToUpdate = _repositoriesWrapper.Books.Get(id);

			if (bookToUpdate == null)
			{
				throw new DbEntityNotFoundException($"Book with id {id} not found in the database.");
			}

			_mapper.Map(updateBookDto, bookToUpdate);
			_repositoriesWrapper.Books.Update(bookToUpdate);

			return bookToUpdate;
		}


		public Book Delete(int id)
		{
			var book = GetById(id);

			if (book == null)
			{
				throw new DbEntityNotFoundException($"Book with id {id} not found in the database.");
			}

			return _repositoriesWrapper.Books.Remove(book);
		}
	}
}

