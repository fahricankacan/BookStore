using System;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.AuthorOperation.Command.UpdateAuther;
using WebApi.DbOperations;
using Xunit;

namespace Application.Author.UpdateAutherTests
{
    public class UpdateAutherCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateAutherCommandValidatorTests(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void WhenAutherNameIsExist_AutherName_ThrowException(int id)
        {
            UpdateAutherCommandValidator validator = new();
            UpdateAutherCommand command = new(_dbContext);
            UpdateAutherViewModel auther = new UpdateAutherViewModel
            {
                Birthday = DateTime.Now.AddYears(-2),
                Name = "Fahrican",
                Surname = "Kaçan"
            };

            command.ModelId = id;
            command.Model = auther;


            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}