using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.DirectorOperations.Queries;
using MovieStoreWebApp.Application.DirectorOperations.Queries.GetDirectorDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryValidationTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidDirectorIdIsGiven_Validator_ShouldReturnError()
        {
            GetDirectorDetailQuery query = new(null, null);
            query.DirectorId = 0;

            GetDirectorDetailQueryValidation validation = new();
            var result = validation.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_Validator_ShouldnotReturnError()
        {
            GetDirectorDetailQuery query = new(null, null);
            query.DirectorId = 2;

            GetDirectorDetailQueryValidation validation = new();
            var result = validation.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}
