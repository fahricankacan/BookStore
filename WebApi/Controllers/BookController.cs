using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetById;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext dbContext, IMapper mapper)
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
            GetByIdCommand command = new(_context, _mapper);
            GetByIdCommandValidator validator = new();
            validator.ValidateAndThrow(id);
            var result = command.Handle(id);
            return Ok(result);

        }


        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand createBookCommand = new(_context, _mapper);
            createBookCommand.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(createBookCommand);
            createBookCommand.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModal updateBook)
        {
            UpdateBookCommand command = new(_context);
            UpdateBookCommandValidator validator = new();
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