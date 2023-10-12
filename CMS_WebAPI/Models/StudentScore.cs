namespace CMS_WebAPI.Models
{
    public class StudentScore
    {
        public int StudentId { get; set; }
        public int TranscriptId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SubjectName { get; set; }
        public float FirstColumnScore { get; set; }
        public float SecondColumnScore { get; set; }
        public float ThirdColumnScore { get; set; }
        public float FourthColumnScore { get; set; }
        public float AverageScore { get; set; }
    }
}
