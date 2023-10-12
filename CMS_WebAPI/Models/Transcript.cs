namespace CMS_WebAPI.Models
{
    public class Transcript
    {
        public int TranscriptId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public float FirstColumnScore { get; set; }
        public float SecondColumnScore { get; set; }
        public float ThirdColumnScore { get; set; }
        public float FourthColumnScore { get; set; }
        public float AvarageScore {  get; set; }
    }
}
