using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCourses();
        Task<Course> AddCourse(Course course);
        Task<bool> DeleteCourse(int CourseId);
        Task<bool> UpdateCourse(Course course);
    }
}
