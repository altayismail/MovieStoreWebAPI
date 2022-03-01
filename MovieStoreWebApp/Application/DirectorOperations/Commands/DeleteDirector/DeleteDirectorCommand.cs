using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        private readonly MovieStoreDbContext _context;

        public DeleteDirectorCommand(MovieStoreDbContext context)
        {
            _context = context;
        }

        public int DirectorId { get; set; }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.Id == DirectorId);

            if (director is null)
                throw new InvalidOperationException("Director that is going to be deleted cannot be found.");

            var movie = _context.Movies.SingleOrDefault(x => x.Director == director);

            if (movie is not null)
                throw new InvalidOperationException("Director that you are going to delete is a director of a movie. Operation has been cancaled.");

            _context.Directors.Remove(director);
            _context.SaveChanges();
        }

    }
}
