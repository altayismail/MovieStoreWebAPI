using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidation : AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidation()
        {
            RuleFor(command => command.ViewModel.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.ViewModel.Surname).NotEmpty().MinimumLength(3);
        }
    }
}
