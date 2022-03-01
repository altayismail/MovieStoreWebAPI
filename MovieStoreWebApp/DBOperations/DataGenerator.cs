using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                List<MovieGenre> movieGenres = new() { 
                    new MovieGenre()
                    {
                        GenreName = "Science Fiction"
                    },
                    new MovieGenre()
                    {
                        GenreName = "Personal Growth"
                    },
                    new MovieGenre()
                    {
                        GenreName = "Horror"
                    }
                };

                List<Actor> actors = new()
                {
                    new Actor()
                    {
                        Name = "İsmail",
                        Surname = "Altay"
                    },
                    new Actor()
                    {
                        Name = "TestName",
                        Surname = "TestSurname"
                    },
                    new Actor()
                    {
                        Name = "TestName2",
                        Surname = "TestName2"
                    },
                    new Actor()
                    {
                        Name = "TestName3",
                        Surname = "TestName3"
                    }
                };

                List<Director> directors = new()
                {
                    new Director()
                    {
                        Name = "TestDirectorName",
                        Surname = "TestDirectorSurname"
                    },
                    new Director()
                    {
                        Name = "TestDirectorName1",
                        Surname = "TestDirectorSurname1"
                    },
                    new Director()
                    {
                        Name = "TestDirectorName2",
                        Surname = "TestDirectorSurname2"
                    }
                };

                List<Movie> movies = new()
                {
                    new Movie()
                    {
                        MovieName = "Movietestname",
                        GenreId = 1,
                        ReleaseYear = DateTime.Now.AddYears(-7),
                        DirectorId = 1,
                        Price = 12.7D
                    },
                    new Movie()
                    {
                        MovieName = "Movietestname1",
                        GenreId = 2,
                        ReleaseYear = DateTime.Now.AddYears(-3),
                        DirectorId = 2,
                        Price = 17.7D
                    },
                    new Movie()
                    {
                        MovieName = "Movietestname2",
                        GenreId = 3,
                        ReleaseYear = DateTime.Now.AddYears(-1),
                        DirectorId = 3,
                        Price = 22.1D
                    },
                };

                List<MovieActor> movieActors = new()
                {
                    new MovieActor()
                    {
                        MovieId = 1,
                        ActorId = 1
                    },
                    new MovieActor()
                    {
                        MovieId = 2,
                        ActorId = 2
                    },
                    new MovieActor()
                    {
                        MovieId = 1,
                        ActorId = 2
                    },
                    new MovieActor()
                    {
                        MovieId = 3,
                        ActorId = 3
                    }
                };

                List<Customer> customers = new()
                {
                    new Customer()
                    {
                        Name = "Testname",
                        Surname = "Testsurname",
                        Email = "testmail@mail.com",
                        Password = "Password-12"
                    },
                    new Customer()
                    {
                        Name = "Testname1",
                        Surname = "Testsurname1",
                        Email = "testmail1@mail.com",
                        Password = "Password-12"
                    },
                    new Customer()
                    {
                        Name = "Testname2",
                        Surname = "Testsurname2",
                        Email = "testmail2@mail.com",
                        Password = "Password-12"
                    }
                };

                List<Order> orders = new()
                {
                    new Order()
                    {
                        CustomerId = 1,
                        MovieId = 1,
                        Date = DateTime.Now.AddDays(-20),
                        Price = 12.7D
                    },
                    new Order()
                    {
                        CustomerId = 2,
                        MovieId = 2,
                        Date = DateTime.Now.AddDays(-10),
                        Price = 17.7D
                    },
                    new Order()
                    {
                        CustomerId = 3,
                        MovieId = 3,
                        Date = DateTime.Now.AddDays(-1),
                        Price = 22.1D
                    }
                };

                List<CustomerFavouriteMovieGenre> customerFavouriteMovieGenres = new()
                {
                    new CustomerFavouriteMovieGenre()
                    {
                        CustomerId = 1,
                        GenreId = 1
                    },
                    new CustomerFavouriteMovieGenre()
                    {
                        CustomerId = 1,
                        GenreId = 2
                    },
                    new CustomerFavouriteMovieGenre()
                    {
                        CustomerId = 1,
                        GenreId = 3
                    },
                    new CustomerFavouriteMovieGenre()
                    {
                        CustomerId = 2,
                        GenreId = 1
                    }
                };

                if (context.MovieGenres.Any())
                    return;

                context.MovieGenres.AddRange(movieGenres);

                if (context.Actors.Any())
                    return;

                context.Actors.AddRange(actors);

                if (context.Directors.Any())
                    return;

                context.Directors.AddRange(directors);

                if (context.Movies.Any())
                    return;

                context.Movies.AddRange(movies);

                if (context.CustomerFavouriteMovieGenres.Any())
                    return;

                context.CustomerFavouriteMovieGenres.AddRange(customerFavouriteMovieGenres);

                if (context.MovieActors.Any())
                    return;

                context.MovieActors.AddRange(movieActors);

                if (context.Customers.Any())
                    return;

                context.Customers.AddRange(customers);

                if (context.Orders.Any())
                    return;

                context.Orders.AddRange(orders);

                context.SaveChanges();
            }

        }
    }
}
