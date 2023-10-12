using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> AddStudent(Student student);
        Task<bool> DeleteStudent(int studentId);
        Task<bool> UpdateStudent(Student student);
        List<Student> SearchStudents(string keyword);
        void AddOrUpdateAvatar(int studentId, string PictureURL);
        void DeleteAvatar(int studentId);
    }
}
