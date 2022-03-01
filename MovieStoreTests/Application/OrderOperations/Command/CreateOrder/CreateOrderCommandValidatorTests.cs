using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.CustomerOperations.Commands.CreateOrder;
using MovieStoreWebApp.Application.OrderOperations.Command.CreateOrder;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.OrderOperations.Command.CreateOrder
{
    public class CreateOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public CreateOrderCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldReturnError()
        {
            CreateOrderCommand command = new(_context, null);
            command.viewModel = new CreateOrderViewModel() { CustomerId = 1, MoiveId = 0 };

            CreateOrderCommandValidation validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidMovieIdIsGiven_Validator_ShouldNotReturnError()
        {
            CreateOrderCommand command = new(_context, null);
            command.viewModel = new CreateOrderViewModel() { MoiveId = 1, CustomerId = 1 };

            CreateOrderCommandValidation validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
