using CMS_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using CMS_WebAPI.Data;

namespace CMS_WebAPI.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly List<Teacher> _teachers;
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<Teacher>> GetAllTeacher()
        {
            return await _dbContext.Teachers.ToListAsync();
        }
        public TeacherService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Teacher> AddTeacher(Teacher teacher)
        {
            _dbContext.Teachers.Add(teacher);
            await _dbContext.SaveChangesAsync();
            return teacher;
        }

        public async Task<bool> DeleteTeacher(int TeacherId)
        {
            var teacher = await _dbContext.Teachers.FindAsync(TeacherId);
            if (teacher == null)
                return false;
            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTeacher(Teacher teacher)
        {
            _dbContext.Entry(teacher).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public List<Teacher> SearchTeachers(string keyword)
        {

            return _dbContext.Teachers
                .Where(s =>
                    s.TeacherId.ToString().Contains(keyword) ||
                    s.TeacherFirstName.Contains(keyword) ||
                    s.TeacherLastName.Contains(keyword))
                .ToList();
        }
        public void AddOrUpdateAvatar(int TeacherId, string teacherPicture)
        {
            var teacher = _dbContext.Teachers.FirstOrDefault(u => u.TeacherId == TeacherId);
            if (teacher != null)
            {
                teacher.TeacherPicture = teacherPicture;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteAvatar(int TeacherId)
        {
            var teacher = _dbContext.Teachers.FirstOrDefault(u => u.TeacherId == TeacherId);
            if (teacher != null)
            {
                teacher.TeacherPicture = "";
                _dbContext.SaveChanges();
            }
        }
    }
}
