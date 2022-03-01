using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MovieName { get; set; }
        public DateTime ReleaseYear { get; set; }
        public double Price { get; set; }

        public int GenreId { get; set; }
        public MovieGenre Genre { get; set; }

        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public List<MovieActor> MovieActors { get; set; }
        
    }
}
