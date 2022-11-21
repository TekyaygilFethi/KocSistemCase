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
