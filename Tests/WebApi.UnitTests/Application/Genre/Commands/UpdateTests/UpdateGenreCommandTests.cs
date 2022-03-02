using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.DbOperations;
using Xunit;

namespace Application.Genre.Commands.UpdateTests
{

    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTest(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }


        [Fact]
        public void WhenGenreNameIsExist_GenreName_ShouldRetrunException()
        {
            UpdateGenreCommand command = new(_dbContext);
            var existGenre = _dbContext.Genres.FirstOrDefault();

            command.Id = existGenre.Id;
            command.Model = new UpdateGenreModel
            {
                Name = existGenre.Name,
                IsActive = existGenre.IsActive
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir kitap mevcut");

        }

        [Fact]
        public void WhenGenreIsNotExist_GenreId_ShouldRetrunException()
        {
            UpdateGenreCommand command = new(_dbContext);
            var existGenre = _dbContext.Genres.FirstOrDefault();

            command.Id = 23232;
            command.Model = new UpdateGenreModel
            {
                Name = existGenre.Name,
                IsActive = existGenre.IsActive
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");
        }

    }
}