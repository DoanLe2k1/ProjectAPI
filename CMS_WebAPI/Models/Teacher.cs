using System.ComponentModel.DataAnnotations;

namespace CMS_WebAPI.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TaxCode { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string TeacherBirthday { get; set; }
        public string TeacherGender { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherPhoneNumber { get; set; }
        public string TeacherAddress { get; set; }
        public string SubjectId { get; set; }
        public string Password { get; set; }
        public string TeacherPicture { get; set; }
    }
}
