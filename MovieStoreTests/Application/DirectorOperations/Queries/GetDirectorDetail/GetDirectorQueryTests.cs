using AutoMapper;
using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.DirectorOperations.Queries;
using MovieStoreWebApp.DBOperations;
using System;
using System.Linq;
using Xunit;

namespace MovieStoreTest.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorQueryTests(CommonTestFixture commonTestFixture)
        {
            _mapper = commonTestFixture.Mapper;
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenDoesNotExistDirectorIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            GetDirectorDetailQuery query = new(_context, _mapper);
            query.DirectorId = 100;

            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().
                And.Message.Should().Be("Director is not in the Store.");
        }

        [Fact]
        public void WhenExistDirectorIsGiven_Director_ShouldBeReturned()
        {
            GetDirectorDetailQuery query = new(_context, _mapper);
            query.DirectorId = 2;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var director = _context.Directors.SingleOrDefault(x => x.Id == query.DirectorId);

            director.Should().NotBeNull();
        }
    }
}
