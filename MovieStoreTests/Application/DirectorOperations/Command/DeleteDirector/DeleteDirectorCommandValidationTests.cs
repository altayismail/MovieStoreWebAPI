using FluentAssertions;
using MovieStoreWebApp.Application.DirectorOperations.Commands.DeleteDirector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.DirectorOperations.Command.DeleteDirector
{
    public class DeleteDirectorCommandValidationTests
    {
        [Fact]
        public void WhenInvalidDirectorIdIsGiven_Validator_ShouldReturnError()
        {
            DeleteDirectorCommand command = new(null);
            command.DirectorId = 0;

            DeleteDirectorCommandValidator validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_Validator_ShouldNotReturnError()
        {
            DeleteDirectorCommand command = new(null);
            command.DirectorId = 2;

            DeleteDirectorCommandValidator validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
