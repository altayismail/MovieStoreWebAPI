using AutoMapper;
using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.MovieOperations.Command.CreateMovie
{
    public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommandTests(CommonTestFixture commonTestFixture)
        {
            _mapper = commonTestFixture.Mapper;
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistMovieNameAndReleasedayIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            CreateMovieCommand command = new(_context, _mapper);
            CreateMovieViewModel viewModel = new() { MovieName = "2021-2022", DirectorId = 1, GenreId = 1, Price = 55.5D, ReleaseYear = new DateTime(2021,02,25) };
            command.viewModel = viewModel;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().
                And.Message.Should().Be("This movie is in the store anyway.");
        }
    }
}
