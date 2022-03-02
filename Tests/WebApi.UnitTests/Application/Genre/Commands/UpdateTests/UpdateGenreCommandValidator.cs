using System.Linq;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.DbOperations;
using Xunit;

namespace Application.Genre.Commands.UpdateTests
{

    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateGenreCommandValidatorTest(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void WhenIdIsNotGreaterThanZero_Id_ShouldRetrunException(int id)
        {
            UpdateGenreCommand command = new(_dbContext);
            var existGenre = _dbContext.Genres.FirstOrDefault();
            UpdateGenreCommandValidator validator = new(id);

            command.Id = id;
            command.Model = new UpdateGenreModel
            {
                Name = existGenre.Name,
                IsActive = existGenre.IsActive
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenGenreNameLessThenFour_GenreName_ThrowException()
        {
            UpdateGenreCommand command = new(_dbContext);
            var existGenre = _dbContext.Genres.FirstOrDefault();
            UpdateGenreCommandValidator validator = new(existGenre.Id);

            command.Id = existGenre.Id;
            command.Model = new UpdateGenreModel
            {
                Name = "fa",
                IsActive = existGenre.IsActive
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }

    }

}
