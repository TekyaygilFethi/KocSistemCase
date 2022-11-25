namespace KUSYS.Data.Business.Services.StudentService
{
    public class StudentCourseUpdateModel
    {
        public Guid StudentId { get; set; }
        public string CourseId { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
