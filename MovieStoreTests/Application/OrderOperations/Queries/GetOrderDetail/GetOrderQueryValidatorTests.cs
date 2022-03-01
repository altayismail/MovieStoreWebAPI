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
    public class GetOrderQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public GetOrderQueryValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenInvalidOrderIdIsGiven_Validator_ShoudlReturnError()
        {
            GetOrderDetailQuery query = new(_context, null);
            query.OrderId = -1;

            GetOrderDetailQueryValidation validations = new();
            var result = validations.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidOrderIdIsGiven_Validator_ShoudlNotReturnError()
        {
            GetOrderDetailQuery query = new(_context, null);
            query.OrderId = 1;

            GetOrderDetailQueryValidation validations = new();
            var result = validations.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}
