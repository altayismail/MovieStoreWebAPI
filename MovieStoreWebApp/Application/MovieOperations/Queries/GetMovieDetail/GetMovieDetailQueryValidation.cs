using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidation : AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidation()
        {
            RuleFor(query => query.MovieId).GreaterThan(0);
        }
    }
}
