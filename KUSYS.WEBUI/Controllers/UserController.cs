using AutoMapper;
using KUSYS.Business.Services.Base;
using KUSYS.Business.Services.Classes;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.Services.UserService;
using KUSYS.Data.POCO;
using KUSYS.Data.Web.Base;
using KUSYS.Helper.WebHelpers;
using KUSYS.WEBUI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace KUSYS.WEBUI.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;
        public UserController(IConfiguration configuration, IUnitOfWork uow, IMapper mapper):base(configuration,uow,mapper)
        {
            _userService=base.GetService<UserService>();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, Route("login")]
        public IActionResult Login(LoginRequestModel requestModel)
        {
            ResponseObject<string> response = new ResponseObject<string>();

            var token = _userService.Login(requestModel);
            response.Data = token;

            return Ok(response);
        }
    }
}
