using FluentValidation;

namespace WebApi.Application.AuthorOperation.Command.CreateAuther
{
    public class CreateAutherCommandValidator : AbstractValidator<CreateAutherCommand>
    {

        public CreateAutherCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty();
            RuleFor(command => command.Model.Surname).NotEmpty();
        }
    }
}