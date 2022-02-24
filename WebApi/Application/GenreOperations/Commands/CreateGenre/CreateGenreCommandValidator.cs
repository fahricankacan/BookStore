using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(c => c.Model.Name).NotEmpty();


        }
    }
}