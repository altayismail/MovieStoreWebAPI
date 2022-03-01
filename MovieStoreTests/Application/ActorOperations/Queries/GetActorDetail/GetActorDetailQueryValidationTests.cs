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
    public class GetActorDetailQueryValidationTests 
    {

        [Fact]
        public void WhenInvalidActorIdIsGiven_Validator_ShouldGiveError()
        {
            GetActorDetailQuery query = new(null,null);
            query.ActorId = -1;

            GetActorDetailQueryValidation validation = new();
            var result = validation.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidActorIdIsGiven_Validator_ShouldNotGiveError()
        {
            GetActorDetailQuery query = new(null,null);
            query.ActorId = 1;

            GetActorDetailQueryValidation validation = new();
            var result = validation.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}
