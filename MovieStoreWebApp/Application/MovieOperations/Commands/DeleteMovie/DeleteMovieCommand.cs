using AutoMapper;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly MovieStoreDbContext _context;
        public int MovieId { get; set; }

        public DeleteMovieCommand(MovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("Movie is not in the Store anyway.");

            var customer = _context.Customers.SingleOrDefault(x => x.TakenMovies.Contains(movie));

            if (customer is not null)
                throw new InvalidOperationException("Movie that you are going to delete has been bought by a customer. Operation has been canceled.");

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

    }
}
