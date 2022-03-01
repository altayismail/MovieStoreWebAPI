using FluentAssertions;
using FluentValidation;
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
    public class UpdateActorCommandValidationTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateActorCommandValidationTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenInvalidActorIdIsGiven_Validator_ShouldGiveError()
        {
            UpdateActorCommand command = new(_context);
            command.ActorId = 0;
            command.viewModel = new UpdateActorViewModel { Name = "Test", Surname = "Test" };

            UpdateActorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("","")]
        [InlineData("Test", "")]
        [InlineData("", "Test")]
        [InlineData(null, null)]
        [InlineData(null, "Test")]
        [InlineData(null, "")]
        [InlineData("", null)]
        public void WhenInvalidInputIsGiven_Validator_ShouldGiveError(string Name, string Surname)
        {
            UpdateActorViewModel viewModel = new() { Name = Name, Surname = Surname };
            UpdateActorCommand command = new(_context);
            command.viewModel = viewModel;
            command.ActorId = 1;

            UpdateActorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
