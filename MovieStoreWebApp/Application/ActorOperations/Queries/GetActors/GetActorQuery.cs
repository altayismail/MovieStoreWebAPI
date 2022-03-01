using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.ActorOperations.Queries.GetActors
{
    public class GetActorQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorViewModel viewModel { get; set; }

        public GetActorQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetActorViewModel> Handle()
        {
            var actor = _context.Actors.Include(x => x.MovieActors).ThenInclude(x => x.Movie).OrderBy(x => x.Id).ToList<Actor>();

            if (actor is null)
                throw new InvalidOperationException("Actors cannot be found.");

            List<GetActorViewModel> result = _mapper.Map<List<GetActorViewModel>>(actor);

            return result;
        }
    }

    public class GetActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<ActorMovieViewModel> actorMovieViewModels { get; set; }
        public struct ActorMovieViewModel
        {
            public int Id { get; set; }
            public string MovieName { get; set; }
        }
    }
}
