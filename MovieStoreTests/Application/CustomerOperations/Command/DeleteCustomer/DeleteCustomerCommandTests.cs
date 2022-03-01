using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.CustomerOperations.Commands.DeleteCustomer;
using MovieStoreWebApp.DBOperations;
using System;
using System.Linq;
using Xunit;

namespace MovieStoreTest.Application.CustomerOperations.Command.DeleteCustomer
{
    public class DeleteCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteCustomerCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenDoesNotExistCustomerIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteCustomerCommand command = new(_context);
            command.CustomerId = -1;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().
                And.Message.Should().Be("Customer that is going to be deleted cannot be found.");
        }

        [Fact]
        public void WhenExistsCustomerIdIsGiven_Customer_ShouldBeDeleted()
        {
            DeleteCustomerCommand command = new(_context);
            command.CustomerId = 2;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var customer = _context.Customers.FirstOrDefault(x => x.Id == command.CustomerId);

            customer.Should().BeNull();
        }
    }
}
