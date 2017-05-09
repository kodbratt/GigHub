using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using GitHub.Persistence;
using GitHub.Core.ViewModels;

namespace GitHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upComingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            var viewModel = new HomeViewModel
            {
                UpComingGigs = upComingGigs,
                ShowActions = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

   
    }
}