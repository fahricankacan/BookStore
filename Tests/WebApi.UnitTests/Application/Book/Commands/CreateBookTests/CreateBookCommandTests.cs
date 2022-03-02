using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TextSetup;

using WebApi.Entities;
using Xunit;
using System;
using WebApi.Application.BookOperations.Command.CreateBook;
using FluentAssertions;
using System.Linq;

namespace Application.Commands.CreateBookTests
{
    public class CreateCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly WebApi.DbOperations.BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateCommandTests(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOpreationException_ShouldBeReturn()
        {
            //arrange(Hazırlık)
            var book = new Book { AuthorId = 1, GenreId = 1, PageCount = 300, PublishDate = new DateTime(1998, 8, 28), Title = "fahri" };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new(_context, _mapper);
            command.Model = new CreateBookViewModel() { Title = book.Title };



            //act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //Arrange
            CreateBookCommand command = new(_context, _mapper);
            CreateBookViewModel newModel = new() { Title = "Hobbit", PageCount = 1000, PublishDate = DateTime.Now.Date.AddYears(-2), AuthorId = 1, GenreId = 2 };
            command.Model = newModel;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(p => p.Title == newModel.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(newModel.PageCount);
            book.PublishDate.Should().Be(newModel.PublishDate);
            book.GenreId.Should().Be(newModel.GenreId);
            book.AuthorId.Should().Be(newModel.AuthorId);
        }

    }
}