using CMS_WebAPI.Models;
using CMS_WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI.Service
{
    public class ScoreTypeService : IScoreTypeService
    {
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<ScoreType>> GetAllScoreTypes()
        {
            return await _dbContext.ScoreTypes.ToListAsync();
        }
        public ScoreTypeService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ScoreType> AddScoreType(ScoreType scoreType)
        {
            _dbContext.ScoreTypes.Add(scoreType);
            await _dbContext.SaveChangesAsync();
            return scoreType;
        }

        public async Task<bool> DeleteScoreType(int scoreTypeId)
        {
            var scoreType = await _dbContext.ScoreTypes.FindAsync(scoreTypeId);
            if (scoreType == null)
                return false;
            _dbContext.ScoreTypes.Remove(scoreType);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateScoreType(ScoreType scoreType)
        {
            _dbContext.Entry(scoreType).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
