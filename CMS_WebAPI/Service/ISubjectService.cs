using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjects();
        Task<Subject> AddSubject(Subject subject);
        Task<bool> DeleteSubject(int SubjectId);
        Task<bool> UpdateSubject(Subject subject);
        List<Subject> SearchSubjects(string keyword);
    }
}
