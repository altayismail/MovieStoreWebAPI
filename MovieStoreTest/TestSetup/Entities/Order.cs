using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreTest.TestSetup.Entities
{
    public static class Order
    {
        public static void AddOrder(this MovieStoreDbContext context)
        {
            context.Orders.AddRange
            (
                new MovieStoreWebApp.Entities.Order { CustomerId = 1, Date = DateTime.Now.AddDays(-12), Price = 24, MovieId = 1},
                new MovieStoreWebApp.Entities.Order { CustomerId = 2, Date = DateTime.Now.AddDays(-11), Price = 42, MovieId = 2},
                new MovieStoreWebApp.Entities.Order { CustomerId = 3, Date = DateTime.Now.AddDays(-9), Price = 2, MovieId = 1 },
                new MovieStoreWebApp.Entities.Order { CustomerId = 1, Date = DateTime.Now.AddDays(-1), Price = 13, MovieId = 3 }
            );
        }
    }
}
