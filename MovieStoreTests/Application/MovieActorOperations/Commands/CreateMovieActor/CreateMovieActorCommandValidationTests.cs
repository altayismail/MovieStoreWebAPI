using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommandValidationTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public CreateMovieActorCommandValidationTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void WhenInvalidMovieIdOrActorIdIsGiven_Validation_ShouldReturnError(int MovieId, int ActorId)
        {
            CreateMovieActorCommand command = new(null, _context);
            command.ViewModel = new CreateMovieActorViewModel() { ActorId = ActorId, MovieId = MovieId };

            CreateMovieActorCommandValidation validations = new();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidMovieIdOrActorIdIsGiven_Validation_ShouldNotReturnError()
        {
            CreateMovieActorCommand command = new(null, _context);
            command.ViewModel = new CreateMovieActorViewModel() { ActorId = 1, MovieId = 1 };

            CreateMovieActorCommandValidation validations = new();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
