using System;
using System.Linq;
using FluentValidation;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperation.Command.UpdateAuther
{
    public class UpdateAutherCommandValidator : AbstractValidator<UpdateAutherCommand>
    {
        public UpdateAutherCommandValidator()
        {
            RuleFor(command => command.ModelId).GreaterThan(0);

        }
    }

}