using FluentValidation;
// using WebApi.Application.BookOperations.Command.DeleteBook;

namespace WebApi.Application.GenreOperations.Command.DeleteGenre
{

    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(r => r.GenreId).GreaterThan(0);
        }
    }
}