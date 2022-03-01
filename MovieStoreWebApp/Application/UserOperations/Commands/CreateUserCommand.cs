using AutoMapper;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.UserOperations.Commands
{
    public class CreateUserCommand
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserViewModel viewModel { get; set; }

        public CreateUserCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Email == viewModel.Email);

            if (customer is not null)
                throw new InvalidOperationException("This Email has already been used.");

            customer = _mapper.Map<Customer>(viewModel);

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
    }
    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
