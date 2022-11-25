namespace KUSYS.Data.Business.Services.StudentService
{
    public class StudentDto
    {
        public Guid StudentId { get; set; }

        public string Fullname { get; set; }

        public string BirthDate { get; set; }

        public string CourseIds { get; set; }

        public string CourseNames { get; set; }

        public string Username { get; set; }
    }
}
