using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Command.DeleteGenre
{

    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _dbContext;


        public DeleteGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Genre bulunamadı");
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();

        }
    }
}