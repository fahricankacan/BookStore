using WebApi.DbOperations;
using System.Linq;
using System;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries.GetById
{

    public class GetByIdCommand
    {

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetByIdCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookViewModel Handle(int id)
        {
            var result = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).FirstOrDefault(x => x.Id == id);
            if (result is null)
                throw new InvalidOperationException("Kitap bulunamadı.");

            // BookViewModel book = new BookViewModel
            // {
            //     Genre = ((Common.GenreEnum)result.GenreId).ToString(),
            //     PageCount = result.PageCount,
            //     PublishDate = result.PublishDate.ToString("dd/MM/yyyy"),
            //     Title = result.Title,
            // };

            BookViewModel book = new BookViewModel();
            return _mapper.Map<BookViewModel>(result);

            // return book;

        }
    }
}