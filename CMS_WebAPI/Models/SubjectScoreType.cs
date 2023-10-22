namespace CMS_WebAPI.Models
{
    public class SubjectScoreType
    {
        public int SubjectScoreTypeId { get; set; }
        public string CourseName { get; set; }
        public string SubjectScoreTypeName { get; set; }
        public int ScoreTypeId { get; set; }
        public int ScoreColumn { get; set; }
        public int RequiredScoreColumn { get; set; } 

    }
}
