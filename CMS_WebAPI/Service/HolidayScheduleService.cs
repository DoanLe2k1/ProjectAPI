using CMS_WebAPI.Models;
using CMS_WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI.Service
{
    public class HolidayScheduleService : IHolidayScheduleService
    {
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<HolidaySchedule>> GetAllHolidaySchedules()
        {
            return await _dbContext.HolidaySchedules.ToListAsync();
        }
        public HolidayScheduleService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HolidaySchedule> AddHolidaySchedule(HolidaySchedule holidaySchedule)
        {
            _dbContext.HolidaySchedules.Add(holidaySchedule);
            await _dbContext.SaveChangesAsync();
            return holidaySchedule;
        }

        public async Task<bool> DeleteHolidaySchedule(int holidayId)
        {
            var holidaySchedule = await _dbContext.HolidaySchedules.FindAsync(holidayId);
            if (holidaySchedule == null)
                return false;
            _dbContext.HolidaySchedules.Remove(holidaySchedule);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateHolidaySchedule(HolidaySchedule holidaySchedule)
        {
            _dbContext.Entry(holidaySchedule).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public List<HolidaySchedule> SearchHolidaySchedules(string keyword)
        {
            return _dbContext.HolidaySchedules.Where(s => s.HolidayName.Contains(keyword)).ToList();
        }
    }
}
