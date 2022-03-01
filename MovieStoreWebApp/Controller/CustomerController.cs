using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieStoreWebApp.Application.CustomerOperations.Commands.DeleteCustomer;
using MovieStoreWebApp.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStoreWebApp.Application.TokenOperations.CreateRefreshToken;
using MovieStoreWebApp.Application.TokenOperations.CreateToken;
using MovieStoreWebApp.Application.UserOperations.Commands;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.TokenSettings;

namespace MovieStoreWebApp.Controller
{
    //[Authorize(Roles ="Admin")]
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CustomerController(MovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            GetCustomerDetailQuery query = new(_mapper,_context);
            query.CustomerId = id;

            GetCustomerDetailQueryValidation validator = new();
            validator.ValidateAndThrow(query);

            var result = query.Handle();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            DeleteCustomerCommand command = new(_context);
            command.CustomerId = id;

            DeleteCustomerCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateCustomer([FromBody] CreateUserViewModel newUser)
        {
            CreateUserCommand command = new(_context, _mapper);
            command.viewModel = newUser;

            CreateUserCommandValidation validation = new();
            validation.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenViewModel login)
        {
            CreateTokenCommand command = new(_context, _mapper, _configuration);
            command.viewModel = login;
            var token = command.Handle();

            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> GetRefreshToken([FromQuery] string token)
        {
            CreateRefreshTokenCommand command = new(_configuration, _context);
            command.RefreshToken = token;

            var result = command.Handle();
            return result;
        }
    }
}
