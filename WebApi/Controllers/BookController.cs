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
            // return _context.Books.OrderBy(x => x.Id).ToList<Book>();
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdCommand command = new(_context, _mapper);
            try
            {
                GetByIdCommandValidator validator = new();
                validator.ValidateAndThrow(id);
                var result = command.Handle(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();

        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand createBookCommand = new(_context, _mapper);

            try
            {
                createBookCommand.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(createBookCommand);
                createBookCommand.Handle();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
                //throw;
            }



        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModal updateBook)
        {

            UpdateBookCommand command = new(_context);
            try
            {
                UpdateBookCommandValidator validator = new();
                validator.ValidateAndThrow(updateBook);
                command.Handle(id, updateBook);

                return Ok();

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            DeleteBookCommand command = new(_context);
            command.BookID = id;
            try
            {
                DeleteBookCommandValidator validator = new();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }

    }
}