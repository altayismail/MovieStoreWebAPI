using AutoMapper;
using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidMovieIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            GetMovieDetailQuery query = new(_context, _mapper);
            query.MovieId = -1;

            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("This movie is not in the Store");
        }
        [Fact]
        public void WhenvalidMovieIdIsGiven_Movie_ShouldBeReturn()
        {
            GetMovieDetailQuery query = new(_context, _mapper);
            query.MovieId = 1;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var result = _context.Movies.Single(x => x.Id == 1);

            result.Should().NotBeNull();
        }
    }
}
