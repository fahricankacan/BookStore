using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetById
{
    public class GetByIdCommandValidator : AbstractValidator<int>
    {
        public GetByIdCommandValidator()
        {
            RuleFor(command => command).GreaterThan(0);
        }
    }
}