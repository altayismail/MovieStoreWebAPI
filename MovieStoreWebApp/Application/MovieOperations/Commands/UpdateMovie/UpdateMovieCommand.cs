using AutoMapper;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly MovieStoreDbContext _context;
        public int MovieId { get; set; }
        public UpdateMovieViewModel viewModel { get; set; }

        public UpdateMovieCommand(MovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("Movie that is going to be uptated cannot be found ");

            movie.MovieName = viewModel.MovieName != default ? viewModel.MovieName : movie.MovieName;
            movie.Price = viewModel.price != default ? viewModel.price : movie.Price;

            _context.SaveChanges();
        }
    }

    public class UpdateMovieViewModel
    {
        public string MovieName { get; set; }
        public double price { get; set; }
    }
}
