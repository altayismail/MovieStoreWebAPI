using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int OrderId { get; set; }
        public GetOrderDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetOrderDetailViewModel Handle()
        {
            var order = _context.Orders.Where(x => x.IsActive).Include(x => x.Movie).Include(x => x.Customer).SingleOrDefault(x => x.Id == OrderId);

            if (order is null)
                throw new InvalidOperationException("Order cannot be found.");

            GetOrderDetailViewModel result = _mapper.Map<GetOrderDetailViewModel>(order);

            return result;
        }
    }

    public class GetOrderDetailViewModel
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public double Price { get; set; }
        public DateTime PillDate { get; set; }
    }
}
