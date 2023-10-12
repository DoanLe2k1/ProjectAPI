namespace CMS_WebAPI.Models
{
    public class HolidaySchedule
    {
        public int HolidayId { get; set; } 
        public string HolidayName { get; set; }
        public string DescriptionHoliday {  get; set; }
        public string HolidayStartingDay { get; set; }
        public string HolidayEndingDay { get; set; }
    }
}
