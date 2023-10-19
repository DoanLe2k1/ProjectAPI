using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_WebAPI.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ParentName { get; set; }
        public string Password { get; set; }
        public string PictureURL { get; set; }
        public int SubjectId { get; set; }
        public int TranscriptId { get; set; }
        public int ScoreTypeId { get; set; }
    }
}
