using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeacher();
        Task<Teacher> AddTeacher(Teacher teacher);
        Task<bool> DeleteTeacher(int TeacherId);
        Task<bool> UpdateTeacher(Teacher teacher);
        List<Teacher> SearchTeachers(string keyword);
        void AddOrUpdateAvatar(int TeacherId, string teacherPicture);
        void DeleteAvatar(int TeacherId);
    }
}
