using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface ISubjectScoreTypeService
    {
        Task<List<SubjectScoreType>> GetAllSubjectScoreTypes();
        Task<SubjectScoreType> AddSubjectScoreType(SubjectScoreType subjectScoreType);
        Task<bool> DeleteSubjectScoreType(int subjectScoreTypeId);
        Task<bool> UpdateSubjectScoreType(SubjectScoreType subjectScoreTypeId);
    }
}
