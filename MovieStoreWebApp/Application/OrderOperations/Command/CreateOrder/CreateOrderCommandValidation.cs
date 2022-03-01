using FluentValidation;
using MovieStoreWebApp.Application.CustomerOperations.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.OrderOperations.Command.CreateOrder
{
    public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation()
        {
            RuleFor(command => command.viewModel.MoiveId).GreaterThan(0);
        }
    }
}
