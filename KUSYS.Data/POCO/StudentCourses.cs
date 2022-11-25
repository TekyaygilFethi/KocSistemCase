using KUSYS.Data.POCO.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS.Data.POCO
{
    [Table("StudentCourses")]
    public class StudentCourses : IModifiable
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
