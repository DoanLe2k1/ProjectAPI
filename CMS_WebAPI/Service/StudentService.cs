using CMS_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using CMS_WebAPI.Data;

namespace CMS_WebAPI.Service
{
    public class StudentService : IStudentService
    {

        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<Student>> GetAllStudents()
        {
            return await _dbContext.Students.ToListAsync();
        }
        public StudentService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> AddStudent(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            var student = await _dbContext.Students.FindAsync(studentId);
            if (student == null)
                return false;
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            _dbContext.Entry(student).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public List<Student> SearchStudents(string keyword)
        {
            int phoneNumber;
            bool isNumeric = int.TryParse(keyword, out phoneNumber);

            return _dbContext.Students
                .Where(s =>
                    s.StudentId.ToString().Contains(keyword) ||
                    s.FirstName.Contains(keyword) ||
                    s.LastName.Contains(keyword) ||
                    s.Email.Contains(keyword) ||
                    (isNumeric && s.PhoneNumber == phoneNumber))
                .ToList();
        }
        public void AddOrUpdateAvatar(int studentId, string pictureURL)
        {
            var student = _dbContext.Students.FirstOrDefault(u => u.StudentId == studentId);
            if (student != null)
            {
                student.PictureURL = pictureURL;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteAvatar(int studentId)
        {
            var student = _dbContext.Students.FirstOrDefault(u => u.StudentId == studentId);
            if (student != null)
            {
                student.PictureURL = "";
                _dbContext.SaveChanges();
            }
        }
    }
}
