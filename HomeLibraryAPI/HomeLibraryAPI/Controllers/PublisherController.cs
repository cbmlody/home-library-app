using AutoMapper;

using HomeLibraryAPI.Contracts;
using HomeLibraryAPI.EF.DTO;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeLibraryAPI.Controllers
{
    [Route("api/publisher")]
    public class PublisherController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public PublisherController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublishers()
        {
            try
            {
                var publishers = await _repository.Publisher.GetAllAsync();
                _logger.LogInfo(string.Format(Resource.ReturnedAllInfo, "publishers"));

                var result = _mapper.Map<IEnumerable<PublisherDto>>(publishers);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(Resource.ReturnedErrorInfo, nameof(GetAllPublishers), ex.Message));
                return StatusCode(500, Resource.ServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(Guid id)
        {
            try
            {
                var publisher = await _repository.Publisher.GetByIdAsync(id);
                _logger.LogInfo(string.Format(Resource.ReturnedSingleInfo, nameof(EF.Models.Publisher), id));

                var result = _mapper.Map<PublisherDto>(publisher);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(Resource.ReturnedErrorInfo, nameof(GetPublisherById), ex.Message));
                return StatusCode(500, Resource.ServerError);
            }
        }
    }
}
