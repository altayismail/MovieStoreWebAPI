using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        private readonly MovieStoreDbContext _context;

        public int ActorId { get; set; }

        public DeleteActorCommand(MovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x => x.Id == ActorId);

            if (actor is null)
                throw new InvalidOperationException("Actor is not in the Store anyway.");

            var movieactor = _context.MovieActors.SingleOrDefault(x => x.ActorId == ActorId);

            if (movieactor is not null)
                throw new InvalidOperationException("Actor that you are going to delete is in an actor list of a movie. Operation has been canceled.");

            _context.Actors.Remove(actor);
            _context.SaveChanges();
        }

        
    }
}
