using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TextSetup;
using WebApi.Application.AuthorOperation.DeleteCommand;
using WebApi.DbOperations;
using Xunit;

namespace Application.Author.Commands.DeleteCommands
{

    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandTest(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenAutherIdIsNotExist_AutheId_ThrowException()
        {
            DeleteAutherCommand command = new(_dbContext);


            command.ModelId = 332;

            FluentActions
               .Invoking(() => command.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı.");
        }

        [Fact]
        public void WhenBookHasAutherId_AutherId_ThrowException()
        {
            DeleteAutherCommand command = new(_dbContext);
            var book = _dbContext.Books.Include(x => x.Author).First();

            command.ModelId = book.AuthorId;

            // bool IsAuthorHaveBook = _dbContext.Books.Include(x => x.Author).Any(p => p.Author.Id == command.ModelId);

            FluentActions
            .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Kitabı olan yazar silinemez.İlk önce kitabı silmeniz gerekiyor.");
        }

    }
}