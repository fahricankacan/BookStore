using WebApi.DbOperations;
using System.Linq;
using System;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;

namespace WebApi.GetById
{

    public class GetByIdCommand
    {

        private readonly BookStoreDbContext _dbContext;

        public GetByIdCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookViewModel GetById(int id)
        {
            var result = _dbContext.Books.FirstOrDefault(x => x.Id == id);
            if (result is null)
                throw new InvalidOperationException("Kitap bulunamadı.");

            BookViewModel book = new BookViewModel
            {
                Genre = ((Common.GenreEnum)result.GenreId).ToString(),
                PageCount = result.PageCount,
                PublishDate = result.PublishDate.ToString("dd/MM/yyyy"),
                Title = result.Title,
            };


            return book;

        }
    }
}