using LearningSystem.Data.Enums;
using LearningSystem.Services.Models.Courses;

namespace LearningSystem.Services.Interfaces
{
    public interface ITrainerService
    {
        CourseWithStudentsSummaryServiceModel CourseDetails(int courseId);

        bool SetGrade(int courseId, string studentId, StudentGrade grade);

        byte[] GetStudentExamSubmission(string studentId, int courseId);

        bool IsCourseTrainer(string trainerId, int courseId);
    }
}
