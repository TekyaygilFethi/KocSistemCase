using System.ComponentModel.DataAnnotations;

namespace KUSYS.Data.Business.Services.UserService
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
