using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApp.Application.CustomerOperations.Commands.CreateOrder;
using MovieStoreWebApp.Application.OrderOperations.Command.CreateOrder;
using MovieStoreWebApp.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStoreWebApp.Application.OrderOperations.Queries.GetOrders;
using MovieStoreWebApp.DBOperations;
using System;

namespace MovieStoreWebApp.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public OrderController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult GetOrders()
        {
            GetOrdersQuery query = new(_context,_mapper);

            return Ok(query.Handle());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            GetOrderDetailQuery query = new(_context,_mapper);
            query.OrderId = id;

            GetOrderDetailQueryValidation validator = new();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderViewModel newViewModel)
        {
            CreateOrderCommand command = new(_context,_mapper);
            command.viewModel = newViewModel;

            CreateOrderCommandValidation validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}
