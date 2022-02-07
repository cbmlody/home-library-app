using AutoMapper;

using HomeLibraryAPI.Contracts;
using HomeLibraryAPI.EF.DTO;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            var author = await _repository.Author.GetByIdAsync(id);

            if (author == null)
            {
                _logger.LogError(string.Format(Resource.ReturnedSingleErrorInfo, nameof(EF.Models.Author), id));
                return NotFound();
            }

            _logger.LogInfo(string.Format(Resource.ReturnedSingleInfo, nameof(EF.Models.Author), id));

            var authorResult = _mapper.Map<AuthorDto>(author);
            return Ok(authorResult);
        }
    }
}
