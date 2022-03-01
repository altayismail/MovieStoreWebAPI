using FluentAssertions;
using MovieStoreWebApp.Application.DirectorOperations.Commands.CreateDirector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.DirectorOperations.Command.CreateDirector
{
    public class CreateDirectorCommandValidatorTests
    {
        [Theory]
        [InlineData("","")]
        [InlineData("a", "a")]
        [InlineData("", "a")]
        [InlineData("a", "")]
        [InlineData("Test", "")]
        [InlineData("", "Test")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string Name, string Surname)
        {
            CreateDirectorCommand command = new(null,null);
            CreateDirectorViewModel viewModel = new() { Name = Name, Surname = Surname };
            command.viewModel = viewModel;

            CreateDirectorCommandValidator validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnError()
        {
            CreateDirectorCommand command = new(null, null);
            CreateDirectorViewModel viewModel = new() { Name = "Testname", Surname = "TestSurname"};
            command.viewModel = viewModel;

            CreateDirectorCommandValidator validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
