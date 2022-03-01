using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebApp.Entities;

namespace MovieStoreTest.TestSetup.Entities
{
    public static class CustomerFavouriteMovieGenre
    {
        public static void AddCustomerFavouriteMovieGenre(this MovieStoreDbContext context)
        {
            List<MovieStoreWebApp.Entities.CustomerFavouriteMovieGenre> customerFavoritGenres = new List<MovieStoreWebApp.Entities.CustomerFavouriteMovieGenre>(){
                    new MovieStoreWebApp.Entities.CustomerFavouriteMovieGenre(){
                        CustomerId = 1,
                        GenreId = 1
                    },
                    new MovieStoreWebApp.Entities.CustomerFavouriteMovieGenre(){
                        CustomerId = 1,
                        GenreId = 2
                    },
                    new MovieStoreWebApp.Entities.CustomerFavouriteMovieGenre(){
                        CustomerId = 1,
                        GenreId = 3
                    },
                    new MovieStoreWebApp.Entities.CustomerFavouriteMovieGenre(){
                        CustomerId = 2,
                        GenreId = 1
                    },
                    new MovieStoreWebApp.Entities.CustomerFavouriteMovieGenre(){
                        CustomerId = 2,
                        GenreId = 3
                    }
                };
            context.CustomerFavouriteMovieGenres.AddRange(customerFavoritGenres);
        }
    }
}
