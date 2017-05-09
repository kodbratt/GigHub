using GigHub.Core.Repositories;
using GigHub.Persistence;
using GitHub.Core.Models;
using GitHub.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace GigHub.Repositories
{
    public class GigRepository : IGigRepository
    {
        private IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetMyGigsWithGenre(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
               .Include(g => g.Attendances.Select(a => a.Attendee))
               .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGig(int gigId, string userId)
        {
            return _context.Gigs.Single(g => g.Id == gigId && g.ArtistId == userId);
        }

        public IEnumerable<Gig> GetUpComingGigsByArtist(string artistId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCanceled).Include(g => g.Genre).ToList();
        }

        public Gig GetGigWithAttendee(string userId, int Id)
        {
            return _context.Gigs.Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == Id && g.ArtistId == userId);
        }
        
        public Gig GetGigWithArtistAndGenre(int id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }
    }
}