using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int Id { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;


        public UpdateGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public void Handle()
        {
            var genre = _dbContext.Genres.Where(x => x.Id == this.Id).FirstOrDefault();
            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı.");

            if (_dbContext.Genres
            .Any(x => x.Name.ToLower() == Model.Name.ToLower()
            && x.Id != genre.Id))
            {
                throw new InvalidOperationException("Aynı isimli bir kitap mevcut");
            }

            genre.Name = Model.Name.Trim() != default ? Model.Name : genre.Name;
            genre.IsActive = Model.IsActive;
            _dbContext.Genres.Update(genre);
            _dbContext.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}