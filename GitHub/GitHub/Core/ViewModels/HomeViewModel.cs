using GitHub.Core.Models;
using System.Collections.Generic;


namespace GitHub.Core.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> UpComingGigs { get; set; }
        public bool ShowActions { get; internal set; }

        

    }
}