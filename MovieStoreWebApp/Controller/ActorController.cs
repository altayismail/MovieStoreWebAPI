using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApp.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApp.Application.ActorOperations.Commands.DeleteActor;
using MovieStoreWebApp.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebApp.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebApp.Application.ActorOperations.Queries.GetActors;
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
    public class ActorController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public ActorController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetActors()
        {
            GetActorQuery query = new(_context,_mapper);

            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetActor(int id)
        {
            GetActorDetailQuery query = new(_context,_mapper);
            query.ActorId = id;

            GetActorDetailQueryValidation validator = new();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult CreateActor([FromBody] CreateActorViewModel newViewModel)
        {
            CreateActorCommand command = new(_context,_mapper);
            command.ViewModel = newViewModel;

            CreateActorCommandValidation validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActor(int id)
        {
            DeleteActorCommand command = new(_context);
            command.ActorId = id;

            DeleteActorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateActor([FromBody] UpdateActorViewModel newViewModel,int id)
        {
            UpdateActorCommand command = new(_context);
            command.ActorId = id;
            command.viewModel = newViewModel;

            UpdateActorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}
