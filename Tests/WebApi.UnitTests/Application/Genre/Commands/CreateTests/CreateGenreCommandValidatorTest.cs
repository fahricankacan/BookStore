using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.DbOperations;
using Xunit;

namespace Application.Genre.Commands.CreateTests
{

    public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateGenreCommandValidatorTest(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }


        [Fact]
        public void WhenNameIsEmpty_NameVariable_ShouldRetrunException()
        {

            CreateGenreCommand command = new(_dbContext);
            CreateGenreViewModel genre = new() { Name = "" };
            CreateGenreCommandValidator validator = new();

            command.Model = _mapper.Map<CreateGenreViewModel>(genre);

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }


    }
}