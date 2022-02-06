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
            try
            {
                var authors = await _repository.Author.GetAllAsync();
                _logger.LogInfo(string.Format(Resource.ReturnedAllInfo, "authors"));

                var authorsResult = _mapper.Map<IEnumerable<AuthorDto>>(authors);
                return Ok(authorsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(Resource.ReturnedErrorInfo, nameof(GetAllAuthors), ex.Message));
                return StatusCode(500, Resource.ServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            try
            {
                var author = await _repository.Author.GetByIdAsync(id);

                if (author == null)
                {
                    _logger.LogError(string.Format(Resource.ReturnedSingleErrorInfo, nameof(EF.Models.Author), id));
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo(string.Format(Resource.ReturnedSingleInfo, nameof(EF.Models.Author), id));

                    var authorResult = _mapper.Map<AuthorDto>(author);
                    return Ok(authorResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(Resource.ReturnedErrorInfo, nameof(GetAuthorById), ex.Message));
                return StatusCode(500, Resource.ServerError);
            }
        }
    }
}
