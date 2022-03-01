using FluentAssertions;
using MovieStoreWebApp.Application.CustomerOperations.Commands.DeleteCustomer;
using Xunit;

namespace MovieStoreTest.Application.CustomerOperations.Command.DeleteCustomer
{
    public class DeleteCustomerCommandValidatorTests 
    {
        [Fact]
        public void WhenInvalidCustomerIdIsGiven_Validator_ShouldReturnError()
        {
            DeleteCustomerCommand command = new(null);
            command.CustomerId = -1;

            DeleteCustomerCommandValidator validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidCustomerIdIsGiven_Validator_ShouldNotReturnError()
        {
            DeleteCustomerCommand command = new(null);
            command.CustomerId = 2;

            DeleteCustomerCommandValidator validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
