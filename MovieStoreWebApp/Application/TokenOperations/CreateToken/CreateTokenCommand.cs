using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.TokenSettings;
using System;
using System.Linq;
using System.Security.Claims;

namespace MovieStoreWebApp.Application.TokenOperations.CreateToken
{
    public class CreateTokenCommand
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenViewModel viewModel { get; set; }

        public CreateTokenCommand(MovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Email == viewModel.Email);

            if(customer is not null)
            {
                TokenHandler tokenHandler = new(_configuration);
                Token token = tokenHandler.CreateAccessToken(customer);

                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Email and Password are wrong.");
            
        }
    }

    public class CreateTokenViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
