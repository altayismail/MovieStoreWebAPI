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
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateMovieCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            UpdateMovieCommand command = new(_context);
            command.MovieId = -1;
            command.viewModel = new UpdateMovieViewModel() { MovieName = "Test", price = 10 };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Movie that is going to be uptated cannot be found ");
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Movie_ShouldBeUpdated()
        {
            UpdateMovieCommand command = new(_context);
            command.MovieId = 1;
            command.viewModel = new UpdateMovieViewModel() { MovieName = "Test", price = 10 };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var movie = _context.Movies.SingleOrDefault(x => x.Id == 1);

            movie.MovieName.Should().Be("Test");
            movie.Price.Should().Be(10);
        }
    }
}
