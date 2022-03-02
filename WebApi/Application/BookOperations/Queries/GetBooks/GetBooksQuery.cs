using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Queries.GetBooks
{

    public class GetBooksQuery
    {

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id).ToList();
            List<BookViewModel> bookViewModelsList = new();
            foreach (var book in bookList)
            {
                bookViewModelsList.Add(_mapper.Map<BookViewModel>(book));
            }
            return bookViewModelsList;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
            public string Auther { get; set; }


        }
    }
}