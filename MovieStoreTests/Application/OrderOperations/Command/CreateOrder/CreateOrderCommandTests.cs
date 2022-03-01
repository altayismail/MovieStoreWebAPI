using AutoMapper;
using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.CustomerOperations.Commands.CreateOrder;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.OrderOperations.Command
{
    public class CreateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandTests(CommonTestFixture commonTestFixture)
        {
            _mapper = commonTestFixture.Mapper;
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeCreated()
        {
            CreateOrderCommand command = new(_context, _mapper);
            command.viewModel = new CreateOrderViewModel() { CustomerId = 1, MoiveId = 2 };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var order = _context.Orders.Last(x => x.CustomerId == 1 && x.MovieId == 2);

            order.Should().NotBeNull();
            order.CustomerId.Should().Be(1);
            order.MovieId.Should().Be(2);
        }
    }
}
