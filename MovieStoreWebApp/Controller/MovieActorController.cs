using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApp.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class MovieActorController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public MovieActorController(MovieStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateMovieActor([FromBody] CreateMovieActorViewModel createMovieActorViewModel)
        {
            CreateMovieActorCommand command = new(_mapper, _context);
            command.ViewModel = createMovieActorViewModel;

            CreateMovieActorCommandValidation validations = new();
            validations.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}
