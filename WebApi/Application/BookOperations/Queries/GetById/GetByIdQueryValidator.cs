using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetById
{
    public class GetByIdQueryValidator : AbstractValidator<int>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(command => command).GreaterThan(0);
        }
    }
}