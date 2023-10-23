namespace CMS_WebAPI.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
