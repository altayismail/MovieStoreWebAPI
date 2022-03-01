using AutoMapper;
using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebApp.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.DirectorOperations.Command.DeleteDirector
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteDirectorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenDoesNotExistDirectorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteDirectorCommand command = new(_context);
            command.DirectorId = 57;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().
                And.Message.Should().Be("Director that is going to be deleted cannot be found.");
        }

        [Fact]
        public void WhenAMovieThatItIsDirectorIsExistTriesToBeDeleted_InvalidOperationException_ShouldBeReturn()
        {
            DeleteDirectorCommand command = new(_context);
            command.DirectorId = 1;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().
                And.Message.Should().Be("Director that you are going to delete is a director of a movie. Operation has been cancaled.");
        }

        [Fact]
        public void WhenExistAndValidDirectorIdIsGiven_Director_ShouldBeDeleted()
        {
            Director director = new() { Name = "Test", Surname = "Test" };
            _context.Directors.Add(director);
            _context.SaveChanges();

            DeleteDirectorCommand command = new(_context);
            command.DirectorId = director.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var result = _context.Directors.SingleOrDefault(x => x.Name == director.Name && x.Surname == director.Surname);

            result.Should().BeNull();
        }
    }
}
