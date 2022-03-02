using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.DbOperations;
using Xunit;

namespace Application.Genre.Queries
{
    public class GetGenreDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _dbContext;

        private readonly IMapper _mapper;


        public GetGenreDetailQueryValidatorTest(CommonTestFixture textFixture)
        {
            _mapper = textFixture.Mapper;
            _dbContext = textFixture.Context;
        }

        [Fact]
        public void WhenGenreIsNotExist_GenreId_ShouldRetrunException()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            var genre = _dbContext.Genres.FirstOrDefault();


            command.GenreId = 2323;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür bulunamadı.");

        }
    }
}