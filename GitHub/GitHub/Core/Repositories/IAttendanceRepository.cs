using GitHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        void Add(Attendance attendance);
        Attendance GetAttendance(int gigId, string userId);
        void Remove(Attendance attendance);
    }
}