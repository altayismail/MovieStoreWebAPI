using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.DBOperations;
using MovieStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.DirectorOperations.Queries
{
    public class GetDirectorDetaiQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorDetaiQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetDirectorViewModel> Handle()
        {
            var director = _context.Directors.Include(x => x.DirectedMovies).OrderBy(x => x.Id).ToList<Director>();

            if (director is null)
                throw new InvalidOperationException("There is no any director in the Store.");

            List<GetDirectorViewModel> result = _mapper.Map<List<GetDirectorViewModel>>(director);

            return result;
        } 
    }

    public class GetDirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<DirectorMovieViewModel> Movies { get; set; }

        public struct DirectorMovieViewModel
        {
            public int Id { get; set; }
            public string MovieName { get; set; }
        }

    }
}
