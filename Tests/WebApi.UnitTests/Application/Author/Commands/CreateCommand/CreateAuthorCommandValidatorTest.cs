using System;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.AuthorOperation.Command.CreateAuther;
using WebApi.DbOperations;
using Xunit;

namespace Application.Author.CreateAutherTests
{
    public class CreateAutherCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateAutherCommandTests(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Theory]

        [InlineData("fahrican", "")]
        [InlineData("", "asdasdasd")]
        [InlineData("", "")]

        public void WhenAutherNameIsExist_AutherName_ThrowException(string name, string surname)
        {
            CreateAutherCommand command = new(_dbContext);
            CreateAutherCommandValidator validator = new();
            CreateAutherViewModel auther = new CreateAutherViewModel
            {
                Birthday = DateTime.Now.AddYears(-2),
                Name = name.Trim(),
                Surname = surname.Trim()
            };


            command.Model = auther;

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
}