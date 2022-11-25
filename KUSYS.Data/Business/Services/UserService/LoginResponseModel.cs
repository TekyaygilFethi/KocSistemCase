using KUSYS.Data.POCO;

namespace KUSYS.Data.Business.Services.UserService
{
    public class LoginResponseModel
    {
        public Guid StudentId { get; set; }

        public UserRoleEnum Role { get; set; }

        public string NameSurname { get; set; }
    }
}
