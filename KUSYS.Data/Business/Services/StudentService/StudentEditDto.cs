namespace KUSYS.Data.Business.Services.StudentService
{
    public class StudentEditDto
    {
        public Guid StudentId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public IEnumerable<string> CourseIds { get; set; }
    }
}
