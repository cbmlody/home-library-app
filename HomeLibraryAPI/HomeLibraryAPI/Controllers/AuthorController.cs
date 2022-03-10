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
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public AuthorController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _repository.Author.GetAllAsync();
            _logger.LogInfo(string.Format(Resource.ReturnedAllInfo, "authors"));

            var authorsResult = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorsResult);
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            var author = await _repository.Author.GetByIdAsync(id);

            if (author is null)
            {
                _logger.LogError(string.Format(Resource.ReturnedSingleErrorInfo, nameof(Author), id));
                return NotFound();
            }

            _logger.LogInfo(string.Format(Resource.ReturnedSingleInfo, nameof(Author), id));

            var authorResult = _mapper.Map<AuthorDto>(author);
            return Ok(authorResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorCreateUpdateDto author)
        {
            if (author is null)
            {
                _logger.LogError("Author object sent from client is null");
                return BadRequest("Author object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid author object sent from client.");
                return BadRequest("Invalid model object");
            }

            var authorEntity = _mapper.Map<Author>(author);

            _repository.Author.Create(authorEntity);
            await _repository.SaveAsync();

            var createdAuthor = _mapper.Map<AuthorDto>(authorEntity);

            return CreatedAtRoute("GetAuthorById", new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorCreateUpdateDto author)
        {
            if (author is null)
            {
                _logger.LogError("Author object sent from client is null");
                return BadRequest("Author object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid author object sent from client");
                return BadRequest("Invalid model object");
            }

            var authorEntity = await _repository.Author.GetByIdAsync(id);
            if (authorEntity is null)
            {
                _logger.LogError($"Author with id: {id} was not found id db");
                return NotFound();
            }

            _mapper.Map(author, authorEntity);

            _repository.Author.Update(authorEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var authorEntity = await _repository.Author.GetByIdAsync(id);
            if (authorEntity is null)
            {
                _logger.LogError($"Author with id; {id} has not been found in db");
                return NotFound();
            }

            _repository.Author.Delete(authorEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
