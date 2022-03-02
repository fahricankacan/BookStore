using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Command.CreateBook
{
    public class CreateBookCommand
    {

        public CreateBookViewModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
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
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }

    }

    public class CreateBookViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublishDate { get; set; }

        public static implicit operator string(CreateBookViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}