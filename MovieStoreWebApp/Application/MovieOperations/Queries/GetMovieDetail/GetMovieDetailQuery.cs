using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery
    {
        
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMovieDetailViewModel viewModel { get; set; }
        public int MovieId { get; set; }

        public GetMovieDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetMovieDetailViewModel Handle()
        {
            var movie = _context.Movies.Include(x => x.Director).Include(x => x.MovieActors).ThenInclude(x => x.Actor).Include(x => x.Genre).SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("This movie is not in the Store");

            GetMovieDetailViewModel movies = _mapper.Map<GetMovieDetailViewModel>(movie);

            return movies;
        }


        public class GetMovieDetailViewModel
        {
            public int Id { get; set; }
            public string MovieName { get; set; }

            public int ReleaseDate { get; set; }
            public string MovieGenre { get; set; }
            public string Director { get; set; }
            public decimal Price { get; set; }
            public List<MovieActorViewModel> movieActorViewModels { get; set; }

            public struct MovieActorViewModel
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Surname { get; set; }

            }
        }
    }
}
