using KUSYS.Data.POCO;
using System.ComponentModel.DataAnnotations;

namespace KUSYS.Data.Business.Services.StudentService
{
    public class CreateStudentModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Firstname can't be longer than 100 characters!")]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Firstname can't be longer than 100 characters!")]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public UserRoleEnum Role { get; set; }
    }
}
