using BusinessLayer.Models.Book;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers
{
	/// <summary>
	/// Controller for managing books.
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookService _bookService;

		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}

		/// <summary>
		/// Get all books.
		/// </summary>
		[AllowAnonymous]
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
		public IActionResult GetAll()
		{
			var books = _bookService.GetAll();
			return Ok(books);
		}

		/// <summary>
		/// Get a book by its ID.
		/// </summary>
		/// <param name="id">The ID of the book.</param>
		[AllowAnonymous]
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
		public IActionResult GetById(int id)
		{
			var book = _bookService.GetById(id);
			return Ok(book);
		}

		/// <summary>
		/// Get a book by its ISBN.
		/// </summary>
		/// <param name="isbn">The ISBN of the book.</param>
		[AllowAnonymous]
		[HttpGet("{isbn}")]
		[ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
		public IActionResult GetByIsbn(string isbn)
		{
			var book = _bookService.GetByIsbn(isbn);
			return Ok(book);
		}

		/// <summary>
		/// Create a new book.
		/// </summary>
		/// <param name="book">The book to create.</param>
		[Authorize]
		[HttpPost]
		[ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
		public IActionResult Create([FromBody] CreateBookDto? book)
		{
			if (book == null)
			{
				return BadRequest();
			}
			var createdBook = _bookService.Create(book);
			return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
		}

		/// <summary>
		/// Update an existing book.
		/// </summary>
		/// <param name="id">The ID of the book to update.</param>
		/// <param name="book">The updated book data.</param>
		[Authorize]
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
		public IActionResult Update(int id, [FromBody] UpdateBookDto? book)
		{
			if (book == null)
			{
				return BadRequest();
			}
			var updatedBook = _bookService.Update(id, book);
			return Ok(updatedBook);
		}

		/// <summary>
		/// Delete a book by its ID.
		/// </summary>
		/// <param name="id">The ID of the book to delete.</param>
		[Authorize]
		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
		public IActionResult Delete(int id)
		{
			var deletedBook = _bookService.Delete(id);
			return Ok(deletedBook);
		}
	}
}
