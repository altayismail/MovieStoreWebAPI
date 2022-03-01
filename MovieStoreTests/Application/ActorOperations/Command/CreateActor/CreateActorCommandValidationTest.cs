using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.ActorOperations.Command.CreateActor
{
    public class CreateActorCommandValidationTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", "Tested")]
        [InlineData("Tested", "")]
        [InlineData("Te", "")]
        [InlineData("", "Te")]
        [InlineData("Tested", "Te")]
        [InlineData("Te", "Tested")]
        public void WhenInvalidInputIsGiven_Validation_ShouldReturnError(string Name, string Surname)
        {
            CreateActorCommand command = new(null, null);

            CreateActorViewModel viewModel = new() { Name = Name, Surname = Surname };
            command.ViewModel = viewModel;

            CreateActorCommandValidation validations = new();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validation_ShouldNotReturnError()
        {
            CreateActorCommand command = new(null, null);

            CreateActorViewModel viewModel = new() { Name = "Tested", Surname = "Tested" };
            command.ViewModel = viewModel;

            CreateActorCommandValidation validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Equals(0);
        }
    }
}
