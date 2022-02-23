using FluentValidation;

namespace WebApi.BookOperations.GetById
{
    public class GetByIdCommandValidator : AbstractValidator<int>
    {
        public GetByIdCommandValidator()
        {
            RuleFor(command => command).GreaterThan(0);
        }
    }
}