using KUSYS.Data.POCO.Base.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS.Data.POCO
{
    [Table("Students")]
    public class Student : BasePOCOEntity<Guid>
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Firstname can't be longer than 100 characters!")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Firstname can't be longer than 100 characters!")]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public ICollection<StudentCourses> Courses { get; set; }


        //public ICollection<Course> Courses { get; set; }
    }
}
