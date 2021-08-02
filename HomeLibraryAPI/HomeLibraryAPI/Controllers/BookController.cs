﻿using AutoMapper;

using HomeLibraryAPI.Contracts;
using HomeLibraryAPI.EF.DTO;

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
            try
            {
                var books = await _repository.Book.GetAllAsync();
                _logger.LogInfo(string.Format(Resource.ReturnedAllInfo, "books"));

                var booksResult = _mapper.Map<IEnumerable<BookDto>>(books);
                return Ok(booksResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(Resource.ReturnedErrorInfo, nameof(GetAllBooks), ex.Message));
                return StatusCode(500, Resource.ServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById(Guid id)
        {
            try
            {
                var book = await _repository.Book.GetByIdAsync(id);
                if (book == null)
                {
                    _logger.LogError(string.Format(Resource.ReturnedSingleErrorInfo, nameof(EF.Models.Book), id));
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo(string.Format(Resource.ReturnedSingleInfo, nameof(EF.Models.Book), id));

                    var bookResult = _mapper.Map<BookDto>(book);
                    return Ok(bookResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(Resource.ReturnedErrorInfo, nameof(GetBookById), ex.Message));
                return StatusCode(500, Resource.ServerError);
            }
        }
    }
}