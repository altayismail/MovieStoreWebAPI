using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.MovieOperations.Queries.GetMovieDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldReturnError()
        {
            GetMovieDetailQuery query = new(null, null);
            query.MovieId = -1;

            GetMovieDetailQueryValidation validations = new();
            var result = validations.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidMovieIdIsGiven_Validator_ShouldNotReturnError()
        {
            GetMovieDetailQuery query = new(null, null);
            query.MovieId = 1;

            GetMovieDetailQueryValidation validations = new();
            var result = validations.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}
