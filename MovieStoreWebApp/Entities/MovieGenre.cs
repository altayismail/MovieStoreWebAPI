using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Entities
{
    public class MovieGenre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GenreName { get; set; }
        public List<Movie> Movies { get; set; }
        public List<CustomerFavouriteMovieGenre> CustomerFavouriteMovieGenres { get; set; }
    }

}
