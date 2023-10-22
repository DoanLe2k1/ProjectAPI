using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface ICheckOutService
    {
        Task<List<CheckOut>> GetAllCheckOuts();
        Task<CheckOut> AddCheckOut(CheckOut checkOut);
        Task<bool> DeleteCheckOut(int checkOutId);
        Task<bool> UpdateScoreType(CheckOut checkOut);
        CheckOut CalculateCheckOut(CheckOut checkOut);
    }
}
