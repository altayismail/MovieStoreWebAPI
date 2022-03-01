using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.DirectorOperations.Command.UpdateDirector
{
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateDirectorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenDoestNotExistDirectorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            UpdateDirectorCommand command = new(_context);
            command.DirectorId = -1;

            command.viewModel = new UpdateDirectorViewModel() { Name = "Test", Surname = "Test" };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And
                .Message.Should().Be("Director that is going to be updated cannot be found.");
        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_Director_ShouldBeUpdated()
        {
            UpdateDirectorCommand command = new(_context);
            command.DirectorId = 1;

            command.viewModel = new UpdateDirectorViewModel() { Name = "Test", Surname = "Test" };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var director = _context.Directors.Single(x => x.Name == "Test" && x.Surname == "Test");

            director.Surname.Should().Be("Test");
            director.Name.Should().Be("Test");
        }
    }
}
