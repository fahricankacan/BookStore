using WebApi.DbOperations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperation.DeleteCommand
{

    public class DeleteAutherCommandValidator : AbstractValidator<DeleteAutherCommand>
    {

        public DeleteAutherCommandValidator()
        {
            RuleFor(command => command.ModelId).GreaterThan(0);
        }
    }

}