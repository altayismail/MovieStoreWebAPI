using AutoMapper;
using MovieStoreWebApp.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApp.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebApp.Application.ActorOperations.Queries.GetActors;
using MovieStoreWebApp.Application.CustomerOperations.Commands;
using MovieStoreWebApp.Application.CustomerOperations.Commands.CreateOrder;
using MovieStoreWebApp.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStoreWebApp.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebApp.Application.DirectorOperations.Queries;
using MovieStoreWebApp.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebApp.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebApp.Application.MovieOperations.Queries.GetMovies;
using MovieStoreWebApp.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStoreWebApp.Application.OrderOperations.Queries.GetOrders;
using MovieStoreWebApp.Application.UserOperations.Commands;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MovieStoreWebApp.Application.MovieOperations.Queries.GetMovieDetail.GetMovieDetailQuery;

namespace MovieStoreWebApp.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Actor
            CreateMap<Actor, GetActorViewModel>().ForMember(dest => dest.actorMovieViewModels, opt => opt.MapFrom(src => src.MovieActors.Select(x => x.Movie).ToList()));
            CreateMap<Movie, GetActorViewModel.ActorMovieViewModel>();
            CreateMap<Actor, GetActorDetailViewModel>().ForMember(dest => dest.actorMovieViewModels, opt => opt.MapFrom(src => src.MovieActors.Select(x => x.Movie).ToList()));
            CreateMap<Movie, GetActorDetailViewModel.ActorMovieViewModel>();
            CreateMap<CreateActorViewModel, Actor>();

            //Movie
            CreateMap<Movie, GetMoviesViewModel>().ForMember(dest => dest.movieActorViewModels, opt => opt.MapFrom(src => src.MovieActors.Select(x => x.Actor).ToList()))
                                                .ForMember(dest => dest.MovieGenre, opt => opt.MapFrom(src => src.Genre.GenreName.ToString()))
                                                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name.ToString() + src.Director.Surname.ToString()));
            CreateMap<Actor, GetMoviesViewModel.MovieActorViewModel>();
            CreateMap<Movie, GetMovieDetailViewModel>().ForMember(dest => dest.movieActorViewModels, opt => opt.MapFrom(src => src.MovieActors.Select(x => x.Actor).ToList()))
                                                .ForMember(dest => dest.MovieGenre, opt => opt.MapFrom(src => src.Genre.GenreName.ToString()))
                                                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name.ToString() + src.Director.Surname.ToString()));
            CreateMap<Actor, GetMovieDetailViewModel.MovieActorViewModel>();
            CreateMap<CreateMovieViewModel, Movie>();
            
            //Director
            CreateMap<Director, GetDirectorViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.DirectedMovies.ToList()));
            CreateMap<Movie, GetDirectorViewModel.DirectorMovieViewModel>();
            CreateMap<Director, GetDirectorDetailViewModel>().ForMember(dest => dest.directorMovieViewModels, opt => opt.MapFrom(src => src.DirectedMovies.ToList()));
            CreateMap<Movie, GetDirectorDetailViewModel.DirectorMovieViewModel>();
            CreateMap<CreateDirectorViewModel, Director>();

            //MovieActor
            CreateMap<CreateMovieActorViewModel, MovieActor>();


            //Order
            CreateMap<Order, GetOrderViewModel>().ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.Surname)).
                ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.MovieName));
            CreateMap<Order, GetOrderDetailViewModel>().ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.Surname)).
                ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.MovieName));
            CreateMap<CreateOrderViewModel, Order>().ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId)).
                ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MoiveId));

            //Customer
            CreateMap<Customer, GetCustomerViewModel>().ForMember(dest => dest.FavouriteMovieGenres, opt => opt.MapFrom(src => src.CustomerFavouriteMovieGenres.Select(x => x.MovieGenre.GenreName).ToList()))
                                                    .ForMember(dest => dest.TakenMovies, opt => opt.MapFrom(src => src.TakenMovies.Select(x => x.MovieName).ToList()))
                                                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                                                    .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                                                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<CreateUserViewModel, Customer>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).
                ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname)).
                ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password)).
                ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).
                ForAllOtherMembers(dest => dest.Ignore());

        }
    }
}
