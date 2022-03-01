using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public GetOrderDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenInvalidOrderIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            GetOrderDetailQuery query = new(_context,null);
            query.OrderId = -1;

            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Order cannot be found.");
        }
    }
}
