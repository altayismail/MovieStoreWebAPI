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
    public class DeleteMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteMovieCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteMovieCommand command = new(_context);
            command.MovieId = -1;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And
                .Message.Should().Be("Movie is not in the Store anyway.");
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteMovieCommand command = new(_context);
            command.MovieId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var actor = _context.Movies.SingleOrDefault(x => x.Id == 1);

            actor.Should().BeNull();
        }
    }
}
