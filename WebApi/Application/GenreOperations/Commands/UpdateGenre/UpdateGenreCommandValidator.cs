using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator(int id)
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name.Trim() != string.Empty);
            RuleFor(command => id).GreaterThan(0);
        }
    }
}