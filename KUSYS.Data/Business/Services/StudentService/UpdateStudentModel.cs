using System.ComponentModel.DataAnnotations;

namespace KUSYS.Data.Business.Services.StudentService
{
    public class UpdateStudentModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Firstname can't be longer than 100 characters!")]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Firstname can't be longer than 100 characters!")]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        public List<string> CourseIds { get; set; }
    }
}
