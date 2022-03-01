using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidation : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidation()
        {
            RuleFor(command => command.viewModel.DirectorId).NotNull().GreaterThan(0);
            RuleFor(command => command.viewModel.GenreId).NotNull().GreaterThan(0);
            RuleFor(command => command.viewModel.MovieName).NotEmpty().NotNull().MinimumLength(2);
            RuleFor(command => command.viewModel.Price).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(command => command.viewModel.ReleaseYear).LessThan(DateTime.Now.Date).NotNull().NotEmpty();
        }

    }
}
