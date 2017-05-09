using System.Collections.Generic;
using GitHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetMyGigsWithGenre(string userId);
        Gig GetGig(int gigId, string userId);
        Gig GetGigWithArtistAndGenre(int id);
        Gig GetGigWithAttendee(string userId, int Id);
        IEnumerable<Gig> GetUpComingGigsByArtist(string id);
    }
}