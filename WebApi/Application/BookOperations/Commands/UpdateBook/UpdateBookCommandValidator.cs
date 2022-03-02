using System;
using FluentValidation;

namespace WebApi.Application.BookOperations.Command.UpdateBook
{

    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookViewModal>
    {
        public UpdateBookCommandValidator(int id)
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command => command.GenreId).LessThan(3);
            RuleFor(command => command.PageCount).GreaterThan(0);
            RuleFor(command => command.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => id).GreaterThan(0);
        }




    }
}