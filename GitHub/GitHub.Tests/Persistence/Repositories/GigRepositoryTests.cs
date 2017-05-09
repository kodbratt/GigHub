
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Repositories;
using GigHub.Persistence;
using Moq;
using GitHub.Core.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using GitHub.Tests.Extensions;
using FluentAssertions;

namespace GitHub.Tests.Persistence.Repositories
{
    
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpComingGigsByArtist_GigsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "100" };
            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpComingGigsByArtist("100");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpComingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };
            _mockGigs.SetSource(new[] { gig });
            gig.Cancel();

            var gigs = _repository.GetUpComingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpComingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });
           

            var gigs = _repository.GetUpComingGigsByArtist(gig.ArtistId = "-");

            gigs.Should().BeEmpty();
        }
        [TestMethod]
        public void GetUpComingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            _mockGigs.SetSource(new[] { gig });
    
            var gigs = _repository.GetUpComingGigsByArtist(gig.ArtistId);

            gigs.Should().Contain(gig);
        }
    }
}
