using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface ITranscriptService
    {
        Task<List<Transcript>> GetAllTranscripts();
        Task<Transcript> AddTranscript(Transcript transcript);
        Task<bool> DeleteTranscript(int transcriptId);
        Task<bool> UpdateTranscript(Transcript transcript);
        List<Transcript> SearchTranscript(string keyword);
        List<StudentScore> GetScore();


    }
}
