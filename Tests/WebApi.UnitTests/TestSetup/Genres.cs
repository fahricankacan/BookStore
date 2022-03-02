using WebApi.DbOperations;
using WebApi.Entities;

namespace TextSetup
{

    public static class Genres
    {
        public static void AddGenre(this BookStoreDbContext context)
        {
            context.Genres.AddRange
                (
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );
        }
    }
}