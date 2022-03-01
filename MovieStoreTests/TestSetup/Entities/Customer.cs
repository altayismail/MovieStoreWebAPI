using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreTest.TestSetup.Entities
{
    public static class Customer
    {
        public static void AddCustomer(this MovieStoreDbContext context)
        {
            context.Customers.AddRange
            (
                new MovieStoreWebApp.Entities.Customer { Name = "Ferman", Surname= "Akgül", TakenMovies = { } },
                new MovieStoreWebApp.Entities.Customer { Name = "Yağmur", Surname = "Sarıgül", TakenMovies = { } },
                new MovieStoreWebApp.Entities.Customer { Name = "Batuhan", Surname = "Mutlugil", TakenMovies = { } }
            );
        }
    }
}
