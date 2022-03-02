using System.Linq;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.BookOperations.Command.UpdateBook;
using Xunit;

namespace Application.Commands.UpdateBookTests
{
    public class UpdateBookCommandValidationTest : IClassFixture<CommonTestFixture>
    {
        private readonly WebApi.DbOperations.BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommandValidationTest(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdIsntGraterThenZero_Validate_ShouldThrowException(int id)
        {
            UpdateBookCommand command = new(_context);
            UpdateBookCommandValidator validator = new(id);

            var book = _context.Books.FirstOrDefault();

            UpdateBookViewModal bookViewModal = new UpdateBookViewModal
            {
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate,
                Title = book.Title
            };

            var result = validator.Validate(bookViewModal);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]

        public void IfBookTitleLenghtHigherThanFour_Validate_ShouldThrowException()
        {

            UpdateBookCommand command = new(_context);
            var book = _context.Books.FirstOrDefault();
            UpdateBookCommandValidator validator = new(book.Id);


            UpdateBookViewModal bookViewModal = new UpdateBookViewModal
            {
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate,
                Title = "sa"
            };

            var result = validator.Validate(bookViewModal);

            result.Errors.Count.Should().BeGreaterThan(0);

        }


        [Fact]
        public void IfDateGreaterThenNow_Validate_ThrowException()
        {

            UpdateBookCommand command = new(_context);
            var book = _context.Books.FirstOrDefault();
            UpdateBookCommandValidator validator = new(book.Id);


            UpdateBookViewModal bookViewModal = new UpdateBookViewModal
            {
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                PageCount = book.PageCount,
                PublishDate = System.DateTime.Now.Date,
                Title = book.Title
            };

            var result = validator.Validate(bookViewModal);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}