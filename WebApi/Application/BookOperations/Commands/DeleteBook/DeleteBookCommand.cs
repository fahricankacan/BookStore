using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Command.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookID { get; set; }
        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookID);
            if (book is null)
                throw new InvalidOperationException("Silinecek kitap bulunamadı.");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}