﻿namespace CMS_WebAPI.Models
{
    public class Classroom
    {
        public int ClassroomId { get; set; }
        public string ClassroomCode { get; set; }
        public string ClassroomName { get; set; }
        public string SchoolYear { get; set;}
        public string Faculty { get; set; }
        public string NumberOfStudents { get; set; }
        public string Status { get; set; }
        public int Tuition { get; set; }
        public string Description { get; set; }
        public string ClassroomPicture { get; set; }
        public bool OpenOptions { get; set; }
    }
}
