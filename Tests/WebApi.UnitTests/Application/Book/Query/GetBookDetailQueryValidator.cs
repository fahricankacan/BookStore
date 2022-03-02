using System;
using AutoMapper;
using FluentAssertions;
using TextSetup;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.DbOperations;
using Xunit;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;

namespace Application.Commands.Boook.Query
{

    //public void {Koşul}_{TestEdilenBirim}_{BeklenenSonuç}()
    public class GetBookDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBookDetailQueryValidatorTest(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenBookIdNotGreaterThanZero_Id_ShouldThrowException(int Id)
        {

            GetByIdQuery query = new GetByIdQuery(_dbContext, _mapper);
            BookViewModel book = new BookViewModel();
            GetByIdQueryValidator validator = new GetByIdQueryValidator();

            var result = validator.Validate(Id);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}