using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TextSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
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

        }
    }
}