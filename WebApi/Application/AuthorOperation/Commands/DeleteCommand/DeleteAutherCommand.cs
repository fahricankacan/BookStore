using WebApi.DbOperations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi.Application.AuthorOperation.DeleteCommand
{

    public class DeleteAutherCommand
    {
        public int ModelId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public DeleteAutherCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var auther = _dbContext.Authors.SingleOrDefault(p => p.Id == ModelId);
            if (auther is null)
                throw new InvalidOperationException("Yazar bulunamadı.");
            bool IsAuthorHaveBook = _dbContext.Books.Include(x => x.Author).Any(p => p.Author.Id == ModelId);
            if (IsAuthorHaveBook is true)
            {
                throw new InvalidOperationException("Kitabı olan yazar silinemez.İlk önce kitabı silmeniz gerekiyor.");
            }

            _dbContext.Authors.Remove(auther);
            _dbContext.SaveChanges();
        }
    }
}