namespace CMS_WebAPI.Models
{
    public class Transcript
    {
        public int TranscriptId { get; set; }
        public double FirstColumnScore { get; set; }
        public double SecondColumnScore { get; set; }
        public double ThirdColumnScore { get; set; }
        public double FourthColumnScore { get; set; }
        public double AvarageScore {  get; set; }
    }
}
