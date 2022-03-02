using AutoMapper;
using WebApi.Application.BookOperations.Command.CreateBook;
using WebApi.Entities;
using static WebApi.Application.GenreOperations.Queries.GetGenre.GetGenreQuery;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;
using WebApi.Application.AuthorOperation.Queries.GetAuthors;
using WebApi.Application.AuthorOperation.Queries.GetAuthorDetail;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateBookViewModel, Book>();
            CreateMap<Book, BookViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Auther, opt => opt.MapFrom(src => src.Author.Name + src.Author.Surname));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Author, AuthorViewModel>().ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToString("dd/MM/yyyy")));
            CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToString("dd/MM/yyyy")));


        }
    }
}