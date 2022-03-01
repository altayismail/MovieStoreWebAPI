using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.ActorOperations.Commands.DeleteActor;
using MovieStoreWebApp.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.ActorOperations.Command.DeleteActor
{
    public class DeleteActorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteActorCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenDoesNotExistActorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteActorCommand command = new(_context);
            command.ActorId = -1;

            FluentActions.Invoking(() => command.Handle()).Should().
                Throw<InvalidOperationException>().And.Message.Should().Be("Actor is not in the Store anyway.");
        }

        [Fact]
        public void WhenActorIdThatIsInTheActorListOfAMovieIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteActorCommand command = new(_context);
            command.ActorId = 1;

            FluentActions.Invoking(() => command.Handle()).Should().
                Throw<InvalidOperationException>().And.Message.Should().
                Be("Actor that you are going to delete is in an actor list of a movie. Operation has been canceled.");
        }

        [Fact]
        public void WhenValidIdIsGiven_Actor_ShouldBeDeleted()
        {
            DeleteActorCommand commnad = new(_context);
            Actor actor = new() { Name = "Test2", Surname = "Test2", };
            _context.Actors.Add(actor);
            _context.SaveChanges();
            commnad.ActorId = actor.Id;

            FluentActions.Invoking(() => commnad.Handle()).Invoke();

            var deletedActor = _context.Actors.SingleOrDefault(x => x.Id == actor.Id);

            deletedActor.Should().BeNull();
        }
    }
}
