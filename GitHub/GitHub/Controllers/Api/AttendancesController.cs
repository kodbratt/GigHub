using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GitHub.Persistence;
using GitHub.Core.Models;
using GitHub.Core.Dtos;
using GigHub.Persistence;
using GigHub.Core;

namespace GitHub.Controllers.Api
{
    
    [Authorize]
    public class AttendancesController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        [HttpPost]    
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(dto.GigId, userId);
            
            if (attendance != null)
                return BadRequest("Attendance already exits");
            
             attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = User.Identity.GetUserId()
            };

            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(id, userId);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
