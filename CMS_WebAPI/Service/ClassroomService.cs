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
    }
}
