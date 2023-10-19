using CMS_WebAPI.Data;
using CMS_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI.Service
{
    
    public class ClassroomService : IClassroomService
    {
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<Classroom>> GetAllClassrooms()
        {
            return await _dbContext.Classrooms.ToListAsync();
        }
        public ClassroomService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Classroom> AddClassroom(Classroom classroom)
        {
            _dbContext.Classrooms.Add(classroom);
            await _dbContext.SaveChangesAsync();
            return classroom;
        }

        public async Task<bool> DeleteClassroom(int classroomId)
        {
            var classroom = await _dbContext.Classrooms.FindAsync(classroomId);
            if (classroom == null)
                return false;
            _dbContext.Classrooms.Remove(classroom);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateClassroom(Classroom classroom)
        {
            _dbContext.Entry(classroom).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public List<Classroom> SearchClassrooms(string keyword)
        {

            return _dbContext.Classrooms
                .Where(s =>
                    s.ClassroomCode.Contains(keyword) ||
                    s.ClassroomName.Contains(keyword)).ToList();
        }
        public void AddOrUpdateAvatar(int classroomId, string classroomPicture)
        {
            var classroom = _dbContext.Classrooms.FirstOrDefault(u => u.ClassroomId == classroomId);
            if (classroom != null)
            {
                classroom.ClassroomPicture = classroomPicture;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteAvatar(int classroomId)
        {
            var classroom = _dbContext.Classrooms.FirstOrDefault(u => u.ClassroomId == classroomId);
            if (classroom != null)
            {
                classroom.ClassroomPicture = "";
                _dbContext.SaveChanges();
            }
        }
        public List<ClassroomInfo> GetClassroom()
        {
            var classroom = (from p in _dbContext.Classrooms
                         join pm in _dbContext.Subjects on p.SubjectId equals pm.SubjectId
                         join pd in _dbContext.Students on p.StudentId equals pd.StudentId
                         join pf in _dbContext.Teachers on p.TeacherId equals pf.TeacherId
                         select new ClassroomInfo()
                         {
                             FirstName = pd.FirstName,
                             LastName = pd.LastName,
                             SubjectName = pm.SubjectName,
                             TeacherFirstName = pf.TeacherFirstName,
                             TeacherLastName = pf.TeacherLastName,
                             TeacherPhoneNumber = pf.TeacherPhoneNumber,
                             ClassroomCode = p.ClassroomCode,
                             ClassroomName = p.ClassroomName,
                             SchoolYear = p.SchoolYear,
                             Faculty = p.Faculty,
                             NumberOfStudents = p.NumberOfStudents,
                             Status = p.Status,
                             Description = p.Description

                         }).ToList();
            return classroom;
        }
    }
}
