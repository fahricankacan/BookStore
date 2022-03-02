using System;
using WebApi.DbOperations;
using System.Linq;

namespace WebApi.Application.BookOperations.Command.UpdateBook
{

    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;

        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id, UpdateBookViewModal updatedBook)
        {

            var result = _dbContext.Books.SingleOrDefault(x => x.Id == id);
            if (result is null)
                throw new InvalidOperationException("Kitap bulunamadı.");

            result.GenreId = (updatedBook.GenreId == default) ? result.Id : updatedBook.GenreId;
            result.PageCount = (updatedBook.PageCount == default) ? result.PageCount : updatedBook.PageCount;
            result.PublishDate = (updatedBook.PublishDate == default) ? result.PublishDate : updatedBook.PublishDate;
            result.Title = (updatedBook.Title == default) ? result.Title : updatedBook.Title;

            _dbContext.Books.Update(result);

            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookViewModal
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}