namespace CMS_WebAPI.Models
{
    public class StudentScore
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SubjectName { get; set; }
        public string ScoreName { get; set; }
        public int Coefficient { get; set; }
        public double FirstColumnScore { get; set; }
        public double SecondColumnScore { get; set; }
        public double ThirdColumnScore { get; set; }
        public double FourthColumnScore { get; set; }
        public double AverageScore { get; set; }
        
    }
}
