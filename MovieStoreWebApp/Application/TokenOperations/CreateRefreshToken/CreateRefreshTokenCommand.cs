using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.TokenSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.TokenOperations.CreateRefreshToken
{
    public class CreateRefreshTokenCommand
    {
        private readonly MovieStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public string RefreshToken { get; set; }

        public CreateRefreshTokenCommand(IConfiguration configuration, MovieStoreDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public Token Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

            if (customer is not null)
            {
                TokenHandler tokenHandler = new(_configuration);
                Token token = tokenHandler.CreateAccessToken(customer);

                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Valid Refresh Token could not be found.");
        }
    }
}
