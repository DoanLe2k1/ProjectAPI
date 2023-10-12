using CMS_WebAPI.Models;
using CMS_WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using CMS_WebAPI.Service;

namespace Education_WebAPI.Service
{
    public class SubjectScoreTypeService : ISubjectScoreTypeService
    {
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<SubjectScoreType>> GetAllSubjectScoreTypes()
        {
            return await _dbContext.SubjectScoreTypes.ToListAsync();
        }
        public SubjectScoreTypeService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubjectScoreType> AddSubjectScoreType(SubjectScoreType subjectScoreType)
        {
            _dbContext.SubjectScoreTypes.Add(subjectScoreType);
            await _dbContext.SaveChangesAsync();
            return subjectScoreType;
        }

        public async Task<bool> DeleteSubjectScoreType(int subjectScoreTypeId)
        {
            var subjectScoreType = await _dbContext.SubjectScoreTypes.FindAsync(subjectScoreTypeId);
            if (subjectScoreType == null)
                return false;
            _dbContext.SubjectScoreTypes.Remove(subjectScoreType);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSubjectScoreType(SubjectScoreType subjectScoreType)
        {
            _dbContext.Entry(subjectScoreType).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
