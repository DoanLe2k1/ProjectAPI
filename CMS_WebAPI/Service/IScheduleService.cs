using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface IScheduleService
    {
        void AddSubjectToSchedule(int studentId, int subjectId);
        List<Schedule> GetSchedule(int studentId);
    }
}
