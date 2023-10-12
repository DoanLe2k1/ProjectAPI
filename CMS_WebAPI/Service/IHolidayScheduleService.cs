using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface IHolidayScheduleService 
    {
        Task<List<HolidaySchedule>> GetAllHolidaySchedules();
        Task<HolidaySchedule> AddHolidaySchedule(HolidaySchedule holidaySchedule);
        Task<bool> DeleteHolidaySchedule(int holidayId);
        Task<bool> UpdateHolidaySchedule(HolidaySchedule holidaySchedule);
        List<HolidaySchedule> SearchHolidaySchedules(string keyword);
    }
}
