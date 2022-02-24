using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using AutoMapper;
using FluentValidation;
using WebApi.Application.GenreOperations.Queries.GetGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.Application.GenreOperations.Command.DeleteGenre;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetBooks()
        {
            GetGenreQuery query = new(_mapper, _context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreDetailQuery command = new(_mapper, _context);
            command.GenreId = id;
            GetGenreDetailQueryValidator validator = new();
            validator.ValidateAndThrow(command);
            var result = command.Handle();
            return Ok(result);

        }


        [HttpPost]
        public IActionResult AddBook([FromBody] CreateGenreModel genre)
        {
            CreateGenreCommand createGenreCommand = new(_context);
            createGenreCommand.Model = genre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(createGenreCommand);
            createGenreCommand.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new(_context);
            command.Id = id;
            command.Model = updateGenre;
            UpdateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteGenreCommand command = new(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}