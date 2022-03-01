using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreTest.TestSetup.Entities
{
    public static class Director
    {
        public static void AddDirector(this MovieStoreDbContext context)
        {
            context.Directors.AddRange
            (
                new MovieStoreWebApp.Entities.Director { Name = "Abdullah", Surname = "Avcı", DirectedMovies = { } },
                new MovieStoreWebApp.Entities.Director { Name = "Fatih", Surname = "Terim", DirectedMovies = { } },
                new MovieStoreWebApp.Entities.Director { Name = "Mustafa", Surname = "Denizli", DirectedMovies = { } }
            );
        }
    }
}
