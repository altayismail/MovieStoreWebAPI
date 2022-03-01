using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.MovieOperations.Commands.DeleteMovie;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.MovieOperations.Command.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteMovieCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldReturnError()
        {
            DeleteMovieCommand command = new(null);
            command.MovieId = 0;

            DeleteMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Validator_ShouldNotReturnError()
        {
            DeleteMovieCommand command = new(null);
            command.MovieId = 1;

            DeleteMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
