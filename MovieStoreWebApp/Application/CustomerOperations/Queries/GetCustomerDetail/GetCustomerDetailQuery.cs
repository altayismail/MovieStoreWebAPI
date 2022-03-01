using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieStoreWebApp.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int CustomerId { get; set; }
        public GetCustomerViewModel viewModel { get; set; }

        public GetCustomerDetailQuery(IMapper mapper, MovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public GetCustomerViewModel Handle()
        {
            var customer = _context.Customers.Include(x => x.CustomerFavouriteMovieGenres).ThenInclude(x => x.MovieGenre).
                Include(x => x.TakenMovies).ThenInclude(x => x.MovieActors).ThenInclude(x => x.Movie).
                SingleOrDefault(x => x.Id == CustomerId);

            if (customer is null)
                throw new InvalidOperationException("Customer cannot be found.");

            GetCustomerViewModel result = _mapper.Map<GetCustomerViewModel>(customer);

            return result;
        }
    }

    public class GetCustomerViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<string> TakenMovies { get; set; }
        public List<string> FavouriteMovieGenres { get; set; }
    }
}
