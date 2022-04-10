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
    [Route("api/bookseries")]
    [ApiController]
    public class BookSeriesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public BookSeriesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookSeries()
        {
            var bookSeries = await _repository.BookSeries.GetAllAsync();
            _logger.LogInfo("Returnes all authors from db.");

            var bookSeriesResult = _mapper.Map<IEnumerable<BookSeriesDto>>(bookSeries);
            return Ok(bookSeriesResult);
        }

        [HttpGet("{id}", Name = "GetBookSeriesById")]
        public async Task<IActionResult> GetBookSeriesById(Guid id)
        {
            var bookSerie = await _repository.BookSeries.GetByIdAsync(id);

            if (bookSerie is null)
            {
                _logger.LogError($"Could not find book series with id:{id} in db");
                return NotFound();
            }

            _logger.LogInfo($"Returned book series with id:{id} from db");
            var bookSeriesResult = _mapper.Map<IEnumerable<BookSeriesDto>>(bookSerie);

            return Ok(bookSeriesResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookSeries(BookSeriesCreateUpdateDto bookSeries)
        {
            if (bookSeries is null)
            {
                _logger.LogError("BookSeries object sent from client is null");
                return BadRequest("Book series object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid book series object sent from client");
                return BadRequest("Invalid book series object");
            }

            var bookSeriesEntity = _mapper.Map<BookSeries>(bookSeries);
            _repository.BookSeries.Create(bookSeriesEntity);
            await _repository.SaveAsync();

            var createdBookSeries = _mapper.Map<BookSeriesDto>(bookSeriesEntity);

            return CreatedAtRoute("GetBookSeriesById", new { id = createdBookSeries.Id }, createdBookSeries);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookSeries(Guid id, [FromBody] BookSeriesCreateUpdateDto bookSeries)
        {
            if (bookSeries is null)
            {
                _logger.LogError("Book series object sent from client is null");
                return BadRequest("Book series object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid book series object sent from client");
                return BadRequest("Invalid book series object sent from client");
            }

            var bookSeriesEntity = await _repository.BookSeries.GetByIdAsync(id);
            if (bookSeriesEntity is null)
            {
                _logger.LogError($"Book series with id:{id} was not found in db");
                return NotFound();
            }

            _mapper.Map(bookSeries, bookSeriesEntity);
            _repository.BookSeries.Update(bookSeriesEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookSeries(Guid id)
        {
            var booKSeriesEntity = await _repository.BookSeries.GetByIdAsync(id);
            if (booKSeriesEntity is null)
            {
                _logger.LogError($"Book series with id:{id} was not found in db");
                return NotFound();
            }

            _repository.BookSeries.Delete(booKSeriesEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
