using KUSYS.Data.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Data.Business.Services.StudentService
{
    public class UpdateStudentModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Firstname can't be longer than 100 characters!")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Firstname can't be longer than 100 characters!")]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        public List<string> CourseIds { get; set; }
    }
}
