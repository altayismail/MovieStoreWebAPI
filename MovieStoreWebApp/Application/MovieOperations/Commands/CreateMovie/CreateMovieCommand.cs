using AutoMapper;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        private readonly MovieStoreDbContext _context;

        private readonly IMapper _mapper;
        public CreateMovieViewModel viewModel { get; set; }

        public CreateMovieCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.MovieName == viewModel.MovieName && x.ReleaseYear == viewModel.ReleaseYear);

            if (movie is not null)
                throw new InvalidOperationException("This movie is in the store anyway.");

            movie = _mapper.Map<Movie>(viewModel);

            _context.Movies.Add(movie);
            _context.SaveChanges();
        }
    }

    public class CreateMovieViewModel
    {
        public string MovieName { get; set; }
        public DateTime ReleaseYear { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public double Price { get; set; }
    }
}
