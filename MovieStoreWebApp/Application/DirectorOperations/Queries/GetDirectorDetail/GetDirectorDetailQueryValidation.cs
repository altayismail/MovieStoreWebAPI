using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryValidation : AbstractValidator<GetDirectorDetailQuery>
    {
        public GetDirectorDetailQueryValidation()
        {
            RuleFor(query => query.DirectorId).GreaterThan(0);
        }
    }
}
