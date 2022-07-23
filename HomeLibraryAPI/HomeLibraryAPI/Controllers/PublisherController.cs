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

        [HttpGet("{id}", Name = "GetPublisherById")]
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

        [HttpPost]
        public async Task<IActionResult> CreatePublisher(PublisherCreateUpdateDto publisher)
        {
            if (publisher is null)
            {
                _logger.LogError("Publisher object sent from client is null");
                return BadRequest("Publisher object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid publisher object sent from client.");
                return BadRequest("Invalid model object");
            }

            var publisherEntity = _mapper.Map<Publisher>(publisher);

            _repository.Publisher.Create(publisherEntity);
            await _repository.SaveAsync();

            var createdPublisher = _mapper.Map<PublisherDto>(publisherEntity);

            return CreatedAtRoute("GetPublisherById", new { id = createdPublisher.Id }, createdPublisher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(Guid id, [FromBody] PublisherCreateUpdateDto publisher)
        {
            if (publisher is null)
            {
                _logger.LogError("Publisher object sent from client is null");
                return BadRequest("Publisher object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid publisher object sent from client");
                return BadRequest("Invalid model object");
            }

            var publisherEntity = await _repository.Publisher.GetByIdAsync(id);
            if (publisherEntity is null)
            {
                _logger.LogError($"Publisher with id: {id} was not found id db");
                return NotFound();
            }

            _mapper.Map(publisher, publisherEntity);

            _repository.Publisher.Update(publisherEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            var publisherEntity = await _repository.Publisher.GetByIdAsync(id);
            if (publisherEntity is null)
            {
                _logger.LogError($"Publisher with id; {id} has not been found in db");
                return NotFound();
            }

            _repository.Publisher.Delete(publisherEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
