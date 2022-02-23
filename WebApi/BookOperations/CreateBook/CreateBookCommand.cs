using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {

        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {

            var result = _dbContext.Books.Any(x => x.Title == Model.Title);
            if (result is true)
                throw new InvalidOperationException("Kitap zaten mevcut");

            Book book = new Book();
            book = _mapper.Map<Book>(Model);
            // Book book = new Book
            // {
            //     GenreId = Model.GenreId,
            //     PageCount = Model.PageCount,
            //     PublishDate = Model.PublishDate,
            //     Title = Model.Title,
            // };
            _dbContext.Add(book);
            _dbContext.SaveChanges();

        }

    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}