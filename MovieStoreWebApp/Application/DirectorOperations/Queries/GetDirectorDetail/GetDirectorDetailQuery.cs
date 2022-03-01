using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApp.Application.DirectorOperations.Queries
{
    public class GetDirectorDetailQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int DirectorId { get; set; }
        public GetDirectorDetailViewModel viewModel { get; set; }

        public GetDirectorDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetDirectorDetailViewModel Handle()
        {
            var director = _context.Directors.Include(x => x.DirectedMovies).SingleOrDefault(x => x.Id == DirectorId);

            if (director is null)
                throw new InvalidOperationException("Director is not in the Store.");

            GetDirectorDetailViewModel result = _mapper.Map<GetDirectorDetailViewModel>(director);

            return result;
        }
    }

    public class GetDirectorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<DirectorMovieViewModel> directorMovieViewModels { get; set; }

        public struct DirectorMovieViewModel
        {
            public int Id { get; set; }
            public string MovieName { get; set; }
        }

    }
}
