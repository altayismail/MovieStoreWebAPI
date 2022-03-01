using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryValidation : AbstractValidator<GetOrderDetailQuery>
    {
        public GetOrderDetailQueryValidation()
        {
            RuleFor(query => query.OrderId).GreaterThan(0);
        }
    }
}
