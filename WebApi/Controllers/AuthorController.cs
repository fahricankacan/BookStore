using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperation.Command;
using WebApi.Application.AuthorOperation.Command.CreateAuther;
using WebApi.Application.AuthorOperation.Command.UpdateAuther;
using WebApi.Application.AuthorOperation.DeleteCommand;
using WebApi.Application.AuthorOperation.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperation.Queries.GetAuthors;
using WebApi.Application.OperationAbstract;
using WebApi.DbOperations;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IMapper mapper, IBookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpGet("")]
        public IActionResult GetBooks()
        {
            GetAuthorsQuery query = new(_mapper, _context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateAutherViewModel auther)
        {
            CreateAutherCommand command = new(_context);
            command.Model = auther;
            CreateAutherCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteAutherCommand command = new(_context);
            command.ModelId = id;
            DeleteAutherCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] UpdateAutherViewModel auther, int id)
        {
            UpdateAutherCommand command = new(_context);
            command.Model = auther;
            command.ModelId = id;
            UpdateAutherCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorByIdQuery command = new(_context, _mapper);
            command.ModelId = id;
            GetAuthorByIdQueryValidator validator = new();
            validator.ValidateAndThrow(command);
            var result = command.Handle();
            return Ok(result);
        }
    }

}