namespace LearningSystem.Web.Models.Courses
{
    public class CourseDetailsWithStudentViewModel
    {
        public CourseDetailsViewModel CourseDetails { get; set; }

        public string Userid { get; set; }

        public bool HaveCertificate { get; set; }
    }
}
