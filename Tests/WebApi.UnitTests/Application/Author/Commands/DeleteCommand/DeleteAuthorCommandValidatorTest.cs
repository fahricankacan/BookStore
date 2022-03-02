using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.AuthorOperation.DeleteCommand;
using WebApi.DbOperations;
using Xunit;

namespace Application.Author.Commands.DeleteCommands
{

    public class DeleteAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandValidatorTest(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void WhenAutherNameIsExist_AutherName_ThrowException(int id)
        {
            DeleteAutherCommandValidator validator = new();
            DeleteAutherCommand command = new(_dbContext);

            command.ModelId = id;



            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


    }
}