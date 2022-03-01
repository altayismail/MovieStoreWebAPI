using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreTest.TestSetup.Entities;
using MovieStoreWebApp.Common;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreTest.TestSetup
{
    public class CommonTestFixture
    {
        public MovieStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreDB").Options;
            Context = new MovieStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddActor();
            Context.AddCustomer();
            Context.AddDirector();
            Context.AddMovie();
            Context.AddMovieGenre();
            Context.AddOrder();
            Context.AddCustomerFavouriteMovieGenre();
            Context.AddMovieActors();

            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
