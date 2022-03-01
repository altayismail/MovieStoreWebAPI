using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApp.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebApp.Application.MovieOperations.Commands.DeleteMovie;
using MovieStoreWebApp.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebApp.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebApp.Application.MovieOperations.Queries.GetMovies;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Controller
{
    //[Authorize(Roles ="Admin")]
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public MovieController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            GetMoviesQuery query = new(_context,_mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            GetMovieDetailQuery query = new(_context,_mapper);
            query.MovieId = id;

            GetMovieDetailQueryValidation validator = new();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateMovieViewModel newViewModel)
        {
            CreateMovieCommand command = new(_context,_mapper);
            command.viewModel = newViewModel;

            CreateMovieCommandValidation validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            DeleteMovieCommand command = new(_context);
            command.MovieId = id;

            DeleteMovieCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie([FromBody] UpdateMovieViewModel newViewModel, int id)
        {
            UpdateMovieCommand command = new(_context);
            command.viewModel = newViewModel;
            command.MovieId = id;

            UpdateMovieCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
