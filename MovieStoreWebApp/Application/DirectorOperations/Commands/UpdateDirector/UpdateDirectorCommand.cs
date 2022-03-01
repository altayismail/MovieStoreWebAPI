using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly MovieStoreDbContext _context;
        public int DirectorId { get; set; }
        public UpdateDirectorViewModel viewModel { get; set; }
        public UpdateDirectorCommand(MovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.Id == DirectorId);

            if (director is null)
                throw new InvalidOperationException("Director that is going to be updated cannot be found.");

            director.Name = viewModel.Name != default ? viewModel.Name : director.Name;
            director.Surname = viewModel.Surname != default ? viewModel.Surname : director.Surname;

            _context.SaveChanges();
        }
    }

    public class UpdateDirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
