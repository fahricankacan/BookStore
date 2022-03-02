using System;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.AuthorOperation.Command;
using WebApi.Application.AuthorOperation.Command.CreateAuther;
using WebApi.DbOperations;
using Xunit;
using WebApi.Entities;
namespace Application.Author.CreateAutherTests
{
    public class CreateAutherCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateAutherCommandValidatorTests(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenAutherNameIsExist_AutherName_ThrowException()
        {
            CreateAutherCommand command = new(_dbContext);
            CreateAutherViewModel auther = new CreateAutherViewModel
            {
                Birthday = DateTime.Now.AddYears(-2),
                Name = "Fahrican",
                Surname = "Kaçan"
            };

            _dbContext.Authors.Add(
            new WebApi.Entities.Author
            {
                Birthday = auther.Birthday,
                Name = auther.Name,
                Surname = auther.Surname
            });

            _dbContext.SaveChanges();


            command.Model = auther;



            FluentActions
               .Invoking(() => command.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut.");
        }

    }
}