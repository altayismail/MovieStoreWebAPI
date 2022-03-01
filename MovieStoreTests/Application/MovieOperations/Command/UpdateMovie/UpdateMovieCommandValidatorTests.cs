using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.MovieOperations.Command.UpdateMovie
{
    public class UpdateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateMovieCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldReturnError()
        {
            UpdateMovieCommand command = new(_context);
            command.MovieId = 0;
            command.viewModel = new UpdateMovieViewModel() { MovieName = "Test", price = 10 };

            UpdateMovieCommandValidator validations = new();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("",-1)]
        [InlineData("Test",-1)]
        [InlineData("",1)]
        [InlineData("",-11)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string movieName, int price)
        {
            UpdateMovieCommand command = new(null);
            command.MovieId = 1;
            command.viewModel = new UpdateMovieViewModel()
            {
                MovieName = movieName,
                price = price
            };

            UpdateMovieCommandValidator validations = new();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            UpdateMovieCommand command = new(_context);
            command.MovieId = 1;
            command.viewModel = new UpdateMovieViewModel() { MovieName = "Test", price = 10 };

            UpdateMovieCommandValidator validations = new();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
