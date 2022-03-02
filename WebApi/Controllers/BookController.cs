using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Command.CreateBook;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.Application.BookOperations.Command.DeleteBook;
using AutoMapper;
using FluentValidation;
using WebApi.Application.BookOperations.Command.UpdateBook;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdQuery command = new(_context, _mapper);
            GetByIdQueryValidator validator = new();
            validator.ValidateAndThrow(id);
            var result = command.Handle(id);
            return Ok(result);

        }


        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookViewModel newBook)
        {
            CreateBookCommand createBookCommand = new(_context, _mapper);
            createBookCommand.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(createBookCommand);
            createBookCommand.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModal updateBook)
        {
            UpdateBookCommand command = new(_context);
            UpdateBookCommandValidator validator = new(id);
            validator.ValidateAndThrow(updateBook);
            command.Handle(id, updateBook);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new(_context);
            command.BookID = id;
            DeleteBookCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}