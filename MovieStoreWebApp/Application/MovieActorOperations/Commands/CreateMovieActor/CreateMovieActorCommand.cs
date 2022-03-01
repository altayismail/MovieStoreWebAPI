using AutoMapper;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommand
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateMovieActorViewModel ViewModel { get; set; }

        public CreateMovieActorCommand(IMapper mapper, MovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == ViewModel.MovieId);

            if (movie is null)
                throw new InvalidOperationException("Movie cannot be found.");

            var actor = _context.Actors.SingleOrDefault(x => x.Id == ViewModel.ActorId);

            if (actor is null)
                throw new InvalidOperationException("Actor cannot be found.");

            var movieActor = _context.MovieActors.SingleOrDefault(x => x.ActorId == ViewModel.ActorId && x.MovieId == ViewModel.MovieId);

            if (movieActor is not null)
                throw new InvalidOperationException("This actor is in the actor list of the Movie anyway.");

            movieActor = _mapper.Map<MovieActor>(ViewModel);
            _context.MovieActors.Add(movieActor);

            _context.SaveChanges();
        }
    }

    public class CreateMovieActorViewModel
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
    }
}
