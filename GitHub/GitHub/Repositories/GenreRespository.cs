using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitHub.Persistence;
using GitHub.Core.Models;

namespace GigHub.Repositories
{
    public class GenreRespository : IGenreRepository
    {
        private ApplicationDbContext _context;

        public GenreRespository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres;
        }
    }
}