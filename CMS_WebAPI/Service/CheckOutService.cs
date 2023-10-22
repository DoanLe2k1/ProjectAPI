using CMS_WebAPI.Data;
using CMS_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI.Service
{
    public class CheckOutService : ICheckOutService
    {
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<CheckOut>> GetAllCheckOuts()
        {
            return await _dbContext.Checkouts.ToListAsync();
        }
        public CheckOutService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CheckOut> AddCheckOut(CheckOut checkOut)
        {
            _dbContext.Checkouts.Add(checkOut);
            await _dbContext.SaveChangesAsync();
            return checkOut;
        }

        public async Task<bool> DeleteCheckOut(int checkOutId)
        {
            var checkOut = await _dbContext.Checkouts.FindAsync(checkOutId);
            if (checkOut == null)
                return false;
            _dbContext.Checkouts.Remove(checkOut);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateScoreType(CheckOut checkOut)
        {
            _dbContext.Entry(checkOut).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public CheckOut CalculateCheckOut(CheckOut checkOut)
        {
            checkOut.Total = checkOut.Price - checkOut.Discount;
            checkOut.RemainingAmount = checkOut.Total - checkOut.AmountPaid;
            return checkOut;
        }
    }
}
