using AutoMapper;
using FluentAssertions;
using MovieStoreTest.TestSetup;
using MovieStoreWebApp.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreTest.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidActorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            GetActorDetailQuery query = new(_context,_mapper);
            query.ActorId = -1;

            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor is not in the Store.");
        }

        [Fact]
        public void WhenValidActorIsGiven_Actor_ShouldBeReturned()
        {
            GetActorDetailQuery query = new(_context, _mapper);
            query.ActorId = 1;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var actor = _context.Actors.SingleOrDefault(x => x.Id == 1);

            actor.Should().NotBeNull();
        }
    }
}
