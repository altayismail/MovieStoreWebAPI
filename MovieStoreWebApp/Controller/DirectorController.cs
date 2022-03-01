using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApp.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebApp.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebApp.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebApp.Application.DirectorOperations.Queries;
using MovieStoreWebApp.Application.DirectorOperations.Queries.GetDirectorDetail;
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
    public class DirectorController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DirectorController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDirectors()
        {
            GetDirectorDetaiQuery query = new(_context,_mapper);

            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetDirector(int id)
        {
            GetDirectorDetailQuery query = new(_context,_mapper);
            query.DirectorId = id;

            GetDirectorDetailQueryValidation validator = new();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult CreateDirector([FromBody] CreateDirectorViewModel newViewModel)
        {
            CreateDirectorCommand command = new(_context, _mapper);
            command.viewModel = newViewModel;

            CreateDirectorCommandValidator validator = new();
            validator.Validate(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDirector(int id)
        {
            DeleteDirectorCommand command = new(_context);
            command.DirectorId = id;

            DeleteDirectorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDirector([FromBody] UpdateDirectorViewModel newViewModel, int id)
        {
            UpdateDirectorCommand command = new(_context);
            command.DirectorId = id;
            command.viewModel = newViewModel;

            UpdateDirectorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}
