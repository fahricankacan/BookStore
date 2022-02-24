using AutoMapper;
using WebApi.Application.BookOperations.Command.CreateBook;
using WebApi.Entities;
using static WebApi.Application.GenreOperations.Queries.GetGenre.GetGenreQuery;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

        }
    }
}