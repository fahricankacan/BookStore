using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.BookOperations.Command.DeleteBook;
using WebApi.Entities;
using Xunit;

namespace Application.Commands.DeleteBookTests
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly WebApi.DbOperations.BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTest(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenBookIdNotFound_InvalidOperationException_ShoulBeReturn()
        {


            DeleteBookCommand command = new(_context);
            command.BookID = 100;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı.");

        }

    }
}