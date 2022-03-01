using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryValidation : AbstractValidator<GetActorDetailQuery>
    {
        public GetActorDetailQueryValidation()
        {
            RuleFor(query => query.ActorId).GreaterThan(0);
        }
    }
}
