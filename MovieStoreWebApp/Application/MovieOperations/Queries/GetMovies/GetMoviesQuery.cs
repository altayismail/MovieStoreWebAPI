using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMoviesViewModel viewModel { get; set; }
        
        public GetMoviesQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetMoviesViewModel> Handle()
        {
            var movie = _context.Movies.Include(x => x.MovieActors).ThenInclude(x => x.Actor).Include(x => x.Director).Include(x => x.Genre).OrderBy(x => x.Id).ToList<Movie>();

            if (movie is null)
                throw new InvalidOperationException("There are not movies in the Store.");

            List<GetMoviesViewModel> movies = _mapper.Map<List<GetMoviesViewModel>>(movie);

            return movies;
        }
    }

    public class GetMoviesViewModel
    {
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
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
