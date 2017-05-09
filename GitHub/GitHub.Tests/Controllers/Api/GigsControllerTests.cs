using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GigHub.Core;
using GigHub.Controllers.Api;
using GitHub.Tests.Extensions;
using GigHub.Core.Repositories;
using FluentAssertions;
using System.Web.Http.Results;
using GitHub.Core.Models;

namespace GitHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId = "1";

        public GigsControllerTests()
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }
        [TestMethod]
        public void Cancel_NoGigWithGivenIdExist_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCancel_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(g => g.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelAnothersUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig();
            gig.ArtistId = _userId + "-";

            _mockRepository.Setup(g => g.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig();
            gig.ArtistId = _userId;

            _mockRepository.Setup(g => g.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();

        }
    }
}
