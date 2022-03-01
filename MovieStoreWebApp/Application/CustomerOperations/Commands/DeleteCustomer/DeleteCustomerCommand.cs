using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        private readonly MovieStoreDbContext _context;

        public DeleteCustomerCommand(MovieStoreDbContext context)
        {
            _context = context;
        }

        public int CustomerId { get; set; }

        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == CustomerId);

            if (customer is null)
                throw new InvalidOperationException("Customer that is going to be deleted cannot be found.");

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
