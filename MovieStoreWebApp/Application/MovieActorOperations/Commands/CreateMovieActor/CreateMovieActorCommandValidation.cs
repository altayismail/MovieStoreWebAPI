using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommandValidation : AbstractValidator<CreateMovieActorCommand>
    {
        public CreateMovieActorCommandValidation()
        {
            RuleFor(command => command.ViewModel.ActorId).GreaterThan(0);
            RuleFor(command => command.ViewModel.MovieId).GreaterThan(0);
        }
    }
}
