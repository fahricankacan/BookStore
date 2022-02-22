using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.GetById;
using WebApi.UpdateBook;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("")]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new(_context);
            // return _context.Books.OrderBy(x => x.Id).ToList<Book>();
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdCommand command = new(_context);
            try
            {
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
            CreateBookCommand createBookCommand = new(_context);

            try
            {
                createBookCommand.Model = newBook;
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