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

    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateGenreCommandTest(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }


        [Fact]
        public void WhenNameIsNotFound_Handle_ShouldRetrunException()
        {

            CreateGenreCommand command = new(_dbContext);
            //var genre = _dbContext.Genres.FirstOrDefault();

            command.Model = new CreateGenreViewModel { Name = "hıyar" };

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür zaten mevcut.");

        }


    }
}