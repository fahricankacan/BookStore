using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TextSetup;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using System;
using WebApi.Application.BookOperations.Command.CreateBook;
using FluentAssertions;

namespace Application.Commands.CreateBookTests
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lotr", 23, 1)]
        [InlineData("Lotr", 123, 23)]
        [InlineData("", 2, 5)]
        [InlineData("", 0, 0)]
        [InlineData("", 32, -32)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErros(string title, int pageCount, int genreId)
        {
            //arrange 
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookViewModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            //act method
            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);

            //FluentAssertions
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange 
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookViewModel()
            {
                Title = "Lord Of The Ring",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange 
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookViewModel()
            {
                Title = "Lord Of The Ring",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1,
                AuthorId = 1

            };

            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}