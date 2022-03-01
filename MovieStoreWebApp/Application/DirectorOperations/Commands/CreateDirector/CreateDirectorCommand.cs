using AutoMapper;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        private readonly MovieStoreDbContext _context;

        private readonly IMapper _mapper;
        public CreateDirectorViewModel viewModel { get; set; }

        public CreateDirectorCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.Name == viewModel.Name && x.Surname == viewModel.Surname);

            if (director is not null)
                throw new InvalidOperationException("Given Director has already been saved.");

            director = _mapper.Map<Director>(viewModel);

            _context.Directors.Add(director);
            _context.SaveChanges();
        }
    }

    public class CreateDirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
