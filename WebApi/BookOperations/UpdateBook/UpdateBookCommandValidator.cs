using System;
using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{

    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookModal>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command => command.GenreId).LessThan(3);
            RuleFor(command => command.PageCount).GreaterThan(0);
            RuleFor(command => command.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Title).NotEmpty().MinimumLength(4);
        }
    }
}