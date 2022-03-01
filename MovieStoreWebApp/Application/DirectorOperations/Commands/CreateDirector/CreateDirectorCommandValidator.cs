using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(command => command.viewModel.Name).NotEmpty().MinimumLength(2);
            RuleFor(command => command.viewModel.Surname).NotEmpty().MinimumLength(2);
        }
    }
}
