namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using WebApi.DbOperations;

    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetGenreDetailQuery(IMapper mapper, BookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.Where(x => x.IsActive && x.Id == GenreId).FirstOrDefault();
            if (genre is null)
                throw new InvalidOperationException("Genre bulunamadı");
            var returnObj = _mapper.Map<GenreDetailViewModel>(genre);
            return returnObj;
        }

        public class GenreDetailViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
