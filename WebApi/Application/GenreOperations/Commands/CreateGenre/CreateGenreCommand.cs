using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public CreateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
                throw new InvalidOperationException("Genre zaten mevcut");

            _dbContext.Genres.Add(new Entities.Genre
            {
                Name = Model.Name
            });
            _dbContext.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}