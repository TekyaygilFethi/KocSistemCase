using KUSYS.Business.Services.Base;
using KUSYS.Data.Business.JWT;
using KUSYS.Data.Business.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business.Services.Interfaces
{
    public interface IUserService
    {
        string Login(LoginRequestModel request);
    }
}
