using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface IScoreTypeService
    {
        Task<List<ScoreType>> GetAllScoreTypes();
        Task<ScoreType> AddScoreType(ScoreType scoreType);
        Task<bool> DeleteScoreType(int scoreTypeId);
        Task<bool> UpdateScoreType(ScoreType scoreType);
    }
}
