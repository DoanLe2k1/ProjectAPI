using CMS_WebAPI.Models;
using CMS_WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI.Service
{
    public class TranscriptService : ITranscriptService
    {
        private readonly List<Transcript> _transcript;
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<Transcript>> GetAllTranscripts()
        {
            return await _dbContext.Transcripts.ToListAsync();
        }
        public TranscriptService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Transcript> AddTranscript(Transcript transcript)
        {
            _dbContext.Transcripts.Add(transcript);
            await _dbContext.SaveChangesAsync();
            return transcript;
        }

        public async Task<bool> DeleteTranscript(int TranscriptId)
        {
            var transcript = await _dbContext.Transcripts.FindAsync(TranscriptId);
            if (transcript == null)
                return false;
            _dbContext.Transcripts.Remove(transcript);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTranscript(Transcript transcript)
        {
            _dbContext.Entry(transcript).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public List<Transcript> SearchTranscript(string keyword)
        {

            return _dbContext.Transcripts
                .Where(s =>
                    s.TranscriptId.ToString().Contains(keyword))
                .ToList();
        }
        public List<StudentScore> GetScore()
        {
            var score = (from p in _dbContext.Students
                         join pm in _dbContext.Subjects on p.SubjectId equals pm.SubjectId
                         join pd in _dbContext.Transcripts on p.TranscriptId equals pd.TranscriptId
                         join pe in _dbContext.ScoreTypes on p.ScoreTypeId equals pe.ScoreTypeId
                         select new StudentScore()
                         {
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             SubjectName = pm.SubjectName,
                             ScoreName = pe.ScoreName,
                             Coefficient = pe.Coefficient,
                             FirstColumnScore = pd.FirstColumnScore,
                             SecondColumnScore = pd.SecondColumnScore,
                             ThirdColumnScore = pd.ThirdColumnScore,
                             FourthColumnScore = pd.FourthColumnScore,
                             AverageScore = pd.AvarageScore
                         }).ToList();
            return score;
        }

    }
}
