using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        private readonly MovieStoreDbContext _context;
        public int ActorId { get; set; }
        public UpdateActorViewModel viewModel { get; set; }
        public UpdateActorCommand(MovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x => x.Id == ActorId);

            if (actor is null)
                throw new InvalidOperationException("Actor that is going to be updated cannot be found.");

            actor.Name = viewModel.Name != default ? viewModel.Name : actor.Name;
            actor.Surname = viewModel.Surname != default ? viewModel.Surname : actor.Name;

            _context.SaveChanges();
        }
    }

    public class UpdateActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
