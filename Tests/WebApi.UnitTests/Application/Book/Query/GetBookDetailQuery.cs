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
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommonTestFixture textFixture)
        {
            _dbContext = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenBookIsNotFound_Id_ShouldThrowException()
        {

            GetByIdQuery query = new GetByIdQuery(_dbContext, _mapper);
            BookViewModel book = new BookViewModel();

            FluentActions
                .Invoking(() => query.Handle(-22))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }

    }
}