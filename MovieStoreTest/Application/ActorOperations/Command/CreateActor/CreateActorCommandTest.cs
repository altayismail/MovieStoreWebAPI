using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Linq;
using Xunit;

namespace MovieStoreTest.Application.ActorOperations.Command.CreateActor
{
    [TestClass()]
    public class CreateActorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExisttActorNameandActorSurnameIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            CreateActorCommand command = new(_context, _mapper);
            Actor actor = new Actor() { Name = "Test", Surname = "Test", Movies = { } };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            CreateActorViewModel viewModel = new() { Name = actor.Name, Surname = actor.Surname };
            command.ViewModel = viewModel;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor is in the Store anyway.");
        }

        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeCreated()
        {
            CreateActorCommand command = new(_context, _mapper);

            CreateActorViewModel viewModel = new() { Name = "Test", Surname = "Test", Movies = { } };
            command.ViewModel = viewModel;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var actor = _context.Actors.SingleOrDefault(x => x.Name == "Test" && x.Surname == "Test");

            actor.Should().NotBeNull();
            actor.Name.Should().Be(viewModel.Name);
            actor.Surname.Should().Be(viewModel.Surname);
            actor.Movies.Should().Equals(viewModel.Movies);

        }
    }
}
