using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.DBOperations
{
    public interface IMovieStoreDbContext
    {
        DbSet<Actor> Actors { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Director> Directors { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<MovieGenre> MovieGenres { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<MovieActor> MovieActors { get; set; }
        DbSet<CustomerFavouriteMovieGenre> CustomerFavouriteMovieGenres { get; set; }

        int SaveChanges();
    }
}
