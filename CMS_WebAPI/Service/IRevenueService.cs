using CMS_WebAPI.Models;
namespace CMS_WebAPI.Service
{
    public interface IRevenueService
    {
        Task<List<Revenue>> GetAllRevenues();
        Task<Revenue> AddRevenue(Revenue revenue);
        Task<bool> DeleteRevenue(int revenueId);
        Task<bool> UpdateRevenue(Revenue revenue);
        byte[] GeneratePdf(List<Revenue> revenueData);
    }
}
