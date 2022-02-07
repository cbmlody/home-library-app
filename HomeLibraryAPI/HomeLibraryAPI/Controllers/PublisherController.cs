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
            var publishers = await _repository.Publisher.GetAllAsync();
            _logger.LogInfo(string.Format(Resource.ReturnedAllInfo, "publishers"));

            var result = _mapper.Map<IEnumerable<PublisherDto>>(publishers);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(Guid id)
        {
            var publisher = await _repository.Publisher.GetByIdAsync(id);

            if (publisher == null)
            {
                _logger.LogError(string.Format(Resource.ReturnedSingleErrorInfo, nameof(EF.Models.Publisher), id));
                return NotFound();
            }

            _logger.LogInfo(string.Format(Resource.ReturnedSingleInfo, nameof(EF.Models.Publisher), id));

            var result = _mapper.Map<PublisherDto>(publisher);
            return Ok(result);
        }
    }
}
