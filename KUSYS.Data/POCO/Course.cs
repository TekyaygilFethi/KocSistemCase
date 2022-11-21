using KUSYS.Data.POCO.Base.Classes;
using KUSYS.Data.POCO.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
