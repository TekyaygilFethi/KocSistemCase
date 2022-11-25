using KUSYS.Data.Business.Services.UserService;

namespace KUSYS.Business.Services.Interfaces
{
    public interface IUserService
    {
        LoginResponseModel Login(LoginRequestModel request);
    }
}
