using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStoreWebApp.Entities;

namespace MovieStoreWebApp.DBOperations
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomerFavouriteMovieGenre> CustomerFavouriteMovieGenres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>(ConfigureMovieActor);
            modelBuilder.Entity<CustomerFavouriteMovieGenre>(ConfigureCustomerFavouriteMovieGenre);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureMovieActor(EntityTypeBuilder<MovieActor> modelBuilder)
        {
            modelBuilder.HasKey(x => new { x.MovieId, x.ActorId });
            modelBuilder.HasOne(x => x.Movie).WithMany(x => x.MovieActors).HasForeignKey(x => x.MovieId);
            modelBuilder.HasOne(x => x.Actor).WithMany(x => x.MovieActors).HasForeignKey(x => x.ActorId);
        }

        private void ConfigureCustomerFavouriteMovieGenre(EntityTypeBuilder<CustomerFavouriteMovieGenre> modelBuilder)
        {
            modelBuilder.HasKey(x => new { x.GenreId, x.CustomerId });
            modelBuilder.HasOne<Customer>(x => x.Customer).WithMany(x => x.CustomerFavouriteMovieGenres).HasForeignKey(x => x.CustomerId);
            modelBuilder.HasOne<MovieGenre>(x => x.MovieGenre).WithMany(x => x.CustomerFavouriteMovieGenres).HasForeignKey(x => x.GenreId);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
