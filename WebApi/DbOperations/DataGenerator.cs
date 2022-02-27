using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WebApi.Entities;

namespace WebApi.DbOperations
{

    public class DataGenerator
    {
        public static void Initializer(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Eric",
                        Surname = "Ries",
                        Birthday = new DateTime(1978, 9, 22)
                    },
                new Author
                {
                    Name = "Charlotte Perkins",
                    Surname = "Gilman",
                    Birthday = new DateTime(1980, 7, 3)
                },
                new Author
                {
                    Name = "Frank",
                    Surname = "Helbert",
                    Birthday = new DateTime(1920, 10, 8)
                });
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
                context.Books.AddRange(new Book
                {
                    // Id = 1,
                    Title = "Lean Startup",
                    GenreId = 1,
                    PageCount = 200,
                    AuthorId = 1,
                    PublishDate = new System.DateTime(2001, 06, 12)
                },
                new Book
                {
                    // Id = 2,
                    Title = "Herland",
                    GenreId = 2,
                    PageCount = 250,
                    AuthorId = 2,
                    PublishDate = new System.DateTime(2010, 05, 23)
                },
                new Book
                {
                    // Id = 3,
                    Title = "Dune",
                    GenreId = 2,
                    PageCount = 540,
                    AuthorId = 3,
                    PublishDate = new System.DateTime(2001, 12, 21)
                });

                context.SaveChanges();
            }


        }
    }
}