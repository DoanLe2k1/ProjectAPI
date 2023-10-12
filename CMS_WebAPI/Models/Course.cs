using System.ComponentModel.DataAnnotations;

namespace CMS_WebAPI.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string StartingDay { get; set; }
        public string EndingDay { get; set;}
    }
}
