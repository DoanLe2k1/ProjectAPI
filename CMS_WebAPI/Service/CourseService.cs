using CMS_WebAPI.Models;
using CMS_WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI.Service
{
    public class CourseService : ICourseService
    {
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<Course>> GetAllCourses()
        {
            return await _dbContext.Courses.ToListAsync();
        }
        public CourseService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Course> AddCourse(Course course)
        {
            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();
            return course;
        }

        public async Task<bool> DeleteCourse(int courseId)
        {
            var course = await _dbContext.Courses.FindAsync(courseId);
            if (course == null)
                return false;
            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            _dbContext.Entry(course).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
