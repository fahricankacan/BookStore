using System;
using System.Collections.Generic;
using AutoMapper;
using WebApi.DbOperations;
using System.Linq;

namespace WebApi.Application.AuthorOperation.Queries.GetAuthors
{

    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IMapper mapper, BookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public List<AuthorViewModel> Handle()
        {
            var authors = _dbContext.Authors.OrderBy(x => x.Id);
            return _mapper.Map<List<AuthorViewModel>>(authors);
        }
    }
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
    }
}