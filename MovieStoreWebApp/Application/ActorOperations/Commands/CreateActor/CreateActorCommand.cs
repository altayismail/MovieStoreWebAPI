using AutoMapper;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateActorViewModel ViewModel { get; set; }

        public CreateActorCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x => x.Name == ViewModel.Name && x.Surname == ViewModel.Surname);

            if (actor is not null)
                throw new InvalidOperationException("Actor is in the Store anyway.");

            actor = _mapper.Map<Actor>(ViewModel);

            _context.Actors.Add(actor);
            _context.SaveChanges();
        }
    }

    public class CreateActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
