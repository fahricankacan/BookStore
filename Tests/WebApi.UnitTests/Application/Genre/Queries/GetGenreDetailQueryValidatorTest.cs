using System.Linq;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.DbOperations;
using Xunit;

namespace Application.Genre.Queries
{
    public class GetGenreDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _dbContext;

        private readonly IMapper _mapper;


        public GetGenreDetailQueryTest(CommonTestFixture textFixture)
        {
            _mapper = textFixture.Mapper;
            _dbContext = textFixture.Context;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void WhenGenreIsNotExist_GenreId_ShouldRetrunException(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            var genre = _dbContext.Genres.FirstOrDefault();
            DeleteGenreCommandValidator validator = new();


            command.GenreId = id;

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
}