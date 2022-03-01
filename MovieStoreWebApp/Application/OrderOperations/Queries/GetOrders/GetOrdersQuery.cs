using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetOrdersQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetOrderViewModel> Handle()
        {
            var order = _context.Orders.Where(x => x.IsActive).Include(x => x.Customer).Include(x => x.Movie).OrderBy(x => x.Id).ToList<Order>();

            if (order is null)
                throw new InvalidOperationException("There is no any saved order in DB.");

            List<GetOrderViewModel> result = _mapper.Map<List<GetOrderViewModel>>(order);

            return result;
        }
    }

    public class GetOrderViewModel
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public double Price { get; set; }
        public DateTime PillDate { get; set; }
    }
}
