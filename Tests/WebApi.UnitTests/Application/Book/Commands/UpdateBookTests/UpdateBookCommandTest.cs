using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.BookOperations.Command.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.Commands.UpdateBookTests
{

    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommandTest(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }


        [Fact]
        public void WhenBookIdNotFound_InvalidOperationException_ShoulBeReturn()
        {
            //arrange(Hazırlık)

            var getBook = _context.Books.FirstOrDefault();
            UpdateBookViewModal bookViewModal = new UpdateBookViewModal
            {
                AuthorId = getBook.AuthorId,
                GenreId = getBook.GenreId,
                PageCount = getBook.PageCount,
                PublishDate = getBook.PublishDate,
                Title = getBook.Title,
            };

            UpdateBookCommand command = new(_context);

            FluentActions
                .Invoking(() => command.Handle(22, bookViewModal))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }
    }
}