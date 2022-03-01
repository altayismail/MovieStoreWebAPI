using AutoMapper;
using Microsoft.Extensions.Configuration;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.CustomerOperations.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateOrderViewModel viewModel { get; set; }
        public CreateOrderCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var order = _mapper.Map<Order>(viewModel);

            order.Date = DateTime.Now;
            order.Price = _context.Movies.SingleOrDefault(x => x.Id == viewModel.MoiveId).Price;

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }

    public class CreateOrderViewModel
    {
        public int CustomerId { get; set; }
        public int MoiveId { get; set; }
    }
}
