using AutoMapper;
using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebApp.DBOperations;
using System;
using System.Linq;
using Xunit;

namespace MovieStoreTest.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieActorCommandTests(CommonTestFixture commonTestFixture)
        {
            _mapper = commonTestFixture.Mapper;
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            CreateMovieActorCommand command = new(_mapper, _context);
            command.ViewModel = new CreateMovieActorViewModel { ActorId = 1, MovieId = 0 };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And
                .Message.Should().Be("Movie cannot be found.");
        }

        [Fact]
        public void WhenInvalidActorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            CreateMovieActorCommand command = new(_mapper, _context);
            command.ViewModel = new CreateMovieActorViewModel { ActorId = 0, MovieId = 1 };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And
                .Message.Should().Be("Actor cannot be found.");
        }

        [Fact]
        public void WhenAlreadyInMovieActorListActorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            CreateMovieActorCommand command = new(_mapper, _context);
            command.ViewModel = new CreateMovieActorViewModel { ActorId = 1, MovieId = 1 };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And
                .Message.Should().Be("This actor is in the actor list of the Movie anyway.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Relation_ShouldBeCreated()
        {
            CreateMovieActorCommand command = new(_mapper, _context);
            command.ViewModel = new CreateMovieActorViewModel { ActorId = 2, MovieId = 1 };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var movieActor = _context.MovieActors.SingleOrDefault(x => x.MovieId == 1 && x.ActorId == 2);

            movieActor.ActorId.Should().Be(2);
            movieActor.MovieId.Should().Be(1);
            movieActor.Should().NotBeNull();
        }
    }
}
