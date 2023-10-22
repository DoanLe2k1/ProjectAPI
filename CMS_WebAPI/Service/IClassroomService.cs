using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface IClassroomService
    {
        Task<List<Classroom>> GetAllClassrooms();
        List<ClassroomInfo> GetClassroom();
        Task<Classroom> AddClassroom(Classroom classroom);
        Task<bool> DeleteClassroom(int classroomId);
        Task<bool> UpdateClassroom(Classroom classroom);
        List<Classroom> SearchClassrooms(string keyword);

        void AddOrUpdateAvatar(int classroomId, string PictureURL);
        void DeleteAvatar(int classroomId);
    }
}
