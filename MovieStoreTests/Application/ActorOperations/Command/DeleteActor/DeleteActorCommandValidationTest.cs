using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.ActorOperations.Commands.DeleteActor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.ActorOperations.Command.DeleteActor
{
    public class DeleteActorCommandValidationTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenActorIdIsGivenLessThanZero_Validator_ShouldReturnError()
        {
            DeleteActorCommand command = new(null);
            command.ActorId = -1;

            DeleteActorCommandValidator validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidActorIdIsGiven_Validator_ShouldNotReturnError()
        {
            DeleteActorCommand command = new(null);
            command.ActorId = 1;

            DeleteActorCommandValidator validation = new();
            var result = validation.Validate(command);

            result.Errors.Count.Equals(0);
        }
    }
}
