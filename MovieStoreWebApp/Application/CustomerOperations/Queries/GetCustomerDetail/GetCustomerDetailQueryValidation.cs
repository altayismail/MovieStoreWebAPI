using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryValidation : AbstractValidator<GetCustomerDetailQuery>
    {
        public GetCustomerDetailQueryValidation()
        {
            RuleFor(command => command.CustomerId).GreaterThan(0);
        }
    }
}
