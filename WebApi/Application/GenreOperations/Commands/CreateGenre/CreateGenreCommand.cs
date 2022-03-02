using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public CreateGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is null)
                throw new InvalidOperationException("Tür zaten mevcut.");

            _dbContext.Genres.Add(new Entities.Genre
            {
                Name = Model.Name
            });
            _dbContext.SaveChanges();
        }
    }

    public class CreateGenreViewModel
    {
        public string Name { get; set; }
    }
}