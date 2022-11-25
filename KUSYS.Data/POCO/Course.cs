using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS.Data.POCO
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        public ICollection<StudentCourses> Students { get; set; } = new List<StudentCourses>();
    }
}
