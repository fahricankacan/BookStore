using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperation.Queries.GetAuthorDetail
{
    public class GetAuthorByIdQuery
    {
        public int ModelId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorByIdQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _dbContext.Authors.Where(x => x.Id == ModelId).FirstOrDefault();
            if (author is null)
                throw new InvalidOperationException("Yazar bulunamad─▒.");
            return _mapper.Map<AuthorDetailViewModel>(author);
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
    }
}