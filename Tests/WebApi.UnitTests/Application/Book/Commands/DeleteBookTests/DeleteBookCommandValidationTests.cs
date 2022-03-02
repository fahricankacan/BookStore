using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.BookOperations.Command.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.Commands.DeleteBookTests
{

    public class DeleteBookCommandValidationTests : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandValidationTests(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenBookIdNotExist_InvalidOperationException_ShoulBeReturn()
        {

            DeleteBookCommand command = new(_context);
            DeleteBookCommandValidator validator = new();

            var error = validator.Validate(command);

            error.Errors.Count.Should().BeGreaterThan(0);

        }


        [Theory]
        [InlineDataAttribute(-1)]
        [InlineDataAttribute(0)]

        public void WhenBookIdIsLessThenOne_InvalidOperationException_ShoulBeReturn(int bookId)
        {

            DeleteBookCommand command = new(_context);
            DeleteBookCommandValidator validator = new();
            command.BookID = bookId;

            var error = validator.Validate(command);

            error.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenBookIdMustGreaterThanOne_Book_ShouldBeDeleted()
        {

            //arrange(Hazırlık)
            DeleteBookCommand command = new(_context);
            DeleteBookCommandValidator validator = new();
            var book = new Book { AuthorId = 1, GenreId = 1, PageCount = 300, PublishDate = System.DateTime.Now.AddYears(-5), Title = "Lotr" };


            _context.Books.Add(book);
            _context.SaveChanges();

            var getBook = _context.Books.SingleOrDefault(p => p.Title == book.Title);

            var error = validator.Validate(command);

            command.BookID = getBook.Id;

            FluentActions
              .Invoking(() => command.Handle())
              .Should().NotThrow();
        }

    }
}