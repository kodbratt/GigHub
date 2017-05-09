using FluentAssertions;
using GigHub.Core;
using GigHub.Persistence;
using GigHubIntergrationTests;
using GitHub.Controllers;
using GitHub.Core.Models;
using GigHub.IntergrationTests.Extensions;
using GitHub.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.IntergrationTests.Controllers
{
    [TestFixture]
    public class UnitTest1
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {

            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpComingGigs()
        {
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);
            var genre = _context.Genres.First();
            var gig = new Gig() { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Mine();

            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }
    }
}
