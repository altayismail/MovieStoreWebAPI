using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Entities
{
    [Keyless]
    public class CustomerFavouriteMovieGenre
    {
        public int GenreId { get; set; }
        public MovieGenre MovieGenre { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
