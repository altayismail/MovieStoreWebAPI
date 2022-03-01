using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreTest.TestSetup.Entities
{
    public static class MovieActors
    {
        public static void AddMovieActors(this MovieStoreDbContext context)
        {
            List<MovieStoreWebApp.Entities.MovieActor> movieActors = new List<MovieStoreWebApp.Entities.MovieActor>{
                    new MovieStoreWebApp.Entities.MovieActor
                    {
                        ActorId = 1,
                        MovieId = 1
                    },
                    new MovieStoreWebApp.Entities.MovieActor
                    {
                        ActorId = 3,
                        MovieId = 1
                    },
                    new MovieStoreWebApp.Entities.MovieActor
                    {
                        ActorId = 2,
                        MovieId = 2
                    },
                    new MovieStoreWebApp.Entities.MovieActor
                    {
                        ActorId = 3,
                        MovieId = 3
                    }
                };

            context.MovieActors.AddRange(movieActors);
        }
    }
}
