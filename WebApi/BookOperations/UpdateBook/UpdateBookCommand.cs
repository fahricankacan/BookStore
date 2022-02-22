using System;
using WebApi.DbOperations;
using System.Linq;

namespace WebApi.UpdateBook
{

    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id, UpdateBookModal updatedBook)
        {
            var result = _dbContext.Books.SingleOrDefault(x => x.Id == id);
            if (result is null)
                throw new EntryPointNotFoundException("Kitap bulunamadı");

            result.GenreId = updatedBook.GenreId;
            result.PageCount = updatedBook.PageCount;
            result.PublishDate = updatedBook.PublishDate;
            result.Title = updatedBook.Title;

            _dbContext.Books.Update(result);

            _dbContext.SaveChanges();
        }


    }

    public class UpdateBookModal
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}