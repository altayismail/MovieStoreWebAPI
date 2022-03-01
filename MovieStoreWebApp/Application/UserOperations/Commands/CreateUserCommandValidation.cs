using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.UserOperations.Commands
{
    public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidation()
        {
            RuleFor(command => command.viewModel.Name).NotEmpty().MinimumLength(2);
            RuleFor(command => command.viewModel.Surname).NotEmpty().MinimumLength(2);
            RuleFor(command => command.viewModel.Password).NotEmpty().MinimumLength(7).MaximumLength(16).
                Matches("[A-Z]").WithMessage("You need to use at least an UpperCase letter.").
                Matches("[a-z]").WithMessage("You need to use at least a LowerCase letter.").
                Matches("[0-9]").WithMessage("You need to use at least a Number.").
                Matches("[^a-zA-Z0-9]").WithMessage("You need to use Special Charcters.");
            RuleFor(command => command.viewModel.Email).NotEmpty().Matches("@").Matches(".com").MaximumLength(25).MinimumLength(15);

        }
    }
}
