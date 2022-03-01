using AutoMapper;
using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.DirectorOperations.Command.CreateDirector
{
    public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommandTests(CommonTestFixture commonTestFixture)
        {
            _mapper = commonTestFixture.Mapper;
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistsDirectorNameAndSurnameIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            CreateDirectorCommand comamnd = new(_context,_mapper);
            CreateDirectorViewModel viewModel = new() { Name = "Abdullah", Surname = "Avcı" };
            comamnd.viewModel = viewModel;

            FluentActions.Invoking(() => comamnd.Handle()).Should().Throw<InvalidOperationException>().
                And.Message.Should().Be("Given Director has already been saved.");
        }

        [Fact]
        public void WhenDoesNotExistsDirectorNameAndSurnameIsGiven_Director_ShouldBeCreated()
        {
            CreateDirectorCommand comamnd = new(_context, _mapper);
            CreateDirectorViewModel viewModel = new() { Name = "TestName", Surname = "TestSurname" };
            comamnd.viewModel = viewModel;

            FluentActions.Invoking(() => comamnd.Handle()).Invoke();

            var director = _context.Directors.Single(x => x.Name == "TestName");

            director.Should().NotBeNull();
            director.Name.Should().Be("TestName");
            director.Surname.Should().Be("TestSurname");
        }
    }
}
