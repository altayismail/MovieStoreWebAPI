using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int ActorId { get; set; }

        public GetActorDetailViewModel viewModel { get; set; }

        public GetActorDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetActorDetailViewModel Handle()
        {
            var actor = _context.Actors.Include(x => x.MovieActors).ThenInclude(x => x.Movie).SingleOrDefault(x => x.Id == ActorId);

            if (actor is null)
                throw new InvalidOperationException("Actor is not in the Store.");

            GetActorDetailViewModel result = _mapper.Map<GetActorDetailViewModel>(actor);

            return result;
        }
    }

    public class GetActorDetailViewModel
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
