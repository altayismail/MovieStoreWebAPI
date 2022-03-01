using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreTest.TestSetup.Entities
{
    public static class MovieGenre
    {
        public static void AddMovieGenre(this MovieStoreDbContext context)
        {
            context.MovieGenres.AddRange
            (
                new MovieStoreWebApp.Entities.MovieGenre { GenreName = "Fun"},
                new MovieStoreWebApp.Entities.MovieGenre { GenreName = "Horror" },
                new MovieStoreWebApp.Entities.MovieGenre { GenreName = "Sad" }
            );
        }
    }
}
