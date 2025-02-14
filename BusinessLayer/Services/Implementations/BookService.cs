using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.Book;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;

namespace BusinessLayer.Services.Implementations
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _bookRepository;

		private readonly IMapper _mapper;

		public BookService(IBookRepository bookRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_mapper = mapper;
		}


		public Book GetById(string id)
		{
			var book = _bookRepository.Get(id);
			if (book == null)
			{
				throw new DbEntityNotFoundException($"Book with id {id} not found int the database.");
			}

			return book;
		}
		public IEnumerable<Book> GetAll()
			=> _bookRepository.GetAll();

		public Book GetByIsbn(string isbn)
		{
			var book = _bookRepository.GetByIsbn(isbn);
			if (book == null)
			{
				throw new DbEntityNotFoundException($"Book with ISBN {isbn} not found int the database.");
			}

			return book;
		}

		public Book Create(CreateBookDto createBookDto)
		{
			var isUniqueIsbn = _bookRepository.IsUniqueIsbn(createBookDto.Isbn);

			if (!isUniqueIsbn)
			{
				throw new ItemAlreadyExistsException($"ISBN: '{createBookDto.Isbn}' already exists in database!");
			}

			var book = _mapper.Map<Book>(createBookDto);
			_bookRepository.Save(book);
			return book;
		}

		public Book Update(string id, UpdateBookDto updateBookDto)
		{
			var bookToUpdate = _bookRepository.Get(id);

			if (bookToUpdate == null)
			{
				throw new DbEntityNotFoundException($"Book with id {id} not found in the database.");
			}

			_mapper.Map(updateBookDto, bookToUpdate);
			_bookRepository.Update(bookToUpdate);

			return bookToUpdate;
		}


		public Book Delete(string id)
		{
			var book = GetById(id);

			if (book == null)
			{
				throw new DbEntityNotFoundException($"Book with id {id} not found in the database.");
			}

			return _bookRepository.Remove(book);
		}
	}
}

