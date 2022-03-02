using System;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.AuthorOperation.Command.UpdateAuther;
using WebApi.DbOperations;
using Xunit;

namespace Application.Author.UpdateAutherTests
{
    public class UpdateAutherCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateAutherCommandTests(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenAutherIdIsNotExist_AutheId_ThrowException()
        {
            UpdateAutherCommand command = new(_dbContext);
            UpdateAutherViewModel auther = new UpdateAutherViewModel
            {
                Birthday = DateTime.Now.AddYears(-2),
                Name = "Fahrican",
                Surname = "Kaçan"
            };

            command.ModelId = 332;
            command.Model = auther;



            FluentActions
               .Invoking(() => command.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı.");
        }

    }
}