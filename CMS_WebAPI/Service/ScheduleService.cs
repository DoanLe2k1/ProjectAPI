using CMS_WebAPI.Data;
using CMS_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly CMS_WebAPIDbContext _dbContext;

        public ScheduleService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddSubjectToSchedule(int studentId, int subjectId)
        {
            
            var student = _dbContext.Students.FirstOrDefault(s => s.StudentId == studentId);
            var subject = _dbContext.Subjects.FirstOrDefault(s => s.SubjectId == subjectId);
            // Kiểm tra xem học sinh và môn học có tồn tại trong cơ sở dữ liệu không
            if (student == null || subject == null)
            {
                throw new Exception("Không tìm thấy học sinh hoặc môn học");
            }

            // Tạo một TKB mới
            var schedule = new Schedule
            {
                StudentId = studentId,
                SubjectId = subjectId
            };

            // Thêm TKB vào Database
            _dbContext.Schedules.Add(schedule);
            _dbContext.SaveChanges();
        }

        public List<Schedule> GetSchedule(int studentId)
        {

            // Lấy TKB của sinh viên được chọn từ cơ sở dữ liệu
            var schedule = _dbContext.Schedules
            .Include(s => s.Subject)       // Bao gồm thuộc tính liên quan từ bảng Schedule
            .Include(s => s.Student)       // Bao gồm thuộc tính liên quan từ bảng Student
            .Where(s => s.StudentId == studentId)
            .ToList();
            return schedule;
        }
    }
}
