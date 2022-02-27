using FluentValidation;

namespace WebApi.Application.AuthorOperation.Queries.GetAuthorDetail
{
    public class GetAuthorByIdQueryValidator : AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdQueryValidator()
        {
            RuleFor(command => command.ModelId).GreaterThan(0);
        }
    }
}