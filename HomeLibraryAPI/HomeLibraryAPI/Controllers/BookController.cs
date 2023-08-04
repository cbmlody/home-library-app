using AutoMapper;

using HomeLibraryAPI.Contracts;
using HomeLibraryAPI.EF.DTO;
using HomeLibraryAPI.EF.Models;
using HomeLibraryAPI.EF.UpdateDTO;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeLibraryAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public BookController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBooks()
        {
            var books = await _repository.Book.GetAllAsync();
            _logger.LogInfo(string.Format(Resource.ReturnedAllInfo, "books"));

            var booksResult = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(booksResult);
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<ActionResult> GetBookById(Guid id)
        {
            var book = await _repository.Book.GetByIdAsync(id);

            if (book == null)
            {
                _logger.LogError(string.Format(Resource.ReturnedSingleErrorInfo, nameof(Book), id));
                return NotFound();
            }

            _logger.LogInfo(string.Format(Resource.ReturnedSingleInfo, nameof(Book), id));

            var bookResult = _mapper.Map<BookDto>(book);
            return Ok(bookResult);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(BookCreateUpdateDto book)
        {
            if (book is null)
            {
                _logger.LogError("Book object sent from client is null");
                return BadRequest("Book object is null");
            }

            var bookEntity = _mapper.Map<Book>(book);

            _repository.Book.Create(bookEntity);
            await _repository.SaveAsync();

            var createdBook = _mapper.Map<BookDto>(bookEntity);

            return CreatedAtRoute("GetBookById", new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookCreateUpdateDto book)
        {
            if (book is null)
            {
                _logger.LogError("Book object sent from client is null");
                return BadRequest("Book object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid book object sent from client");
                return BadRequest("Invalid model object");
            }

            var bookEntity = await _repository.Book.GetByIdAsync(id);
            if (bookEntity is null)
            {
                _logger.LogError($"Book with id: {id} was not found id db");
                return NotFound();
            }

            _mapper.Map(book, bookEntity);

            _repository.Book.Update(bookEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var bookEntity = await _repository.Book.GetByIdAsync(id);
            if (bookEntity is null)
            {
                _logger.LogError($"Book with id: {id} could not be found in db");
                return NotFound();
            }

            _repository.Book.Delete(bookEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
