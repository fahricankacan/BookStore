using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TextSetup
{

    public static class Authers
    {
        public static void AddAuthers(this BookStoreDbContext context)
        {
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
        }
    }
}