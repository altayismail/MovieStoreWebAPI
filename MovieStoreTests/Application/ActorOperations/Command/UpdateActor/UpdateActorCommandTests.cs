using AutoMapper;
using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.ActorOperations.Command.UpdateActor
{
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateActorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenWrongActorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            UpdateActorCommand command = new(_context);
            command.ActorId = -1;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.
                Message.Should().Be("Actor that is going to be updated cannot be found.");
        }

        [Fact]
        public void WhenTrueActorIdIsGiven_Actor_ShouldBeUpdated()
        {
            UpdateActorCommand command = new(_context);
            command.ActorId = 2;

            UpdateActorViewModel viewModel = new() { Name = "Test", Surname = "Test"};
            command.viewModel = viewModel;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var actor = _context.Actors.SingleOrDefault(x => x.Id == 2);

            actor.Name.Should().Be("Test");
            actor.Surname.Should().Be("Test");
            actor.Should().NotBeNull();
        }
    }
}
