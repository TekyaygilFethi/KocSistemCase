using AutoMapper;
using KUSYS.Business.Services.Classes;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.Services.UserService;
using KUSYS.WEBUI.Controllers.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KUSYS.WEBUI.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;
        public UserController(IConfiguration configuration, IUnitOfWork uow, IMapper mapper) : base(configuration, uow, mapper)
        {
            _userService = base.GetService<IUserService>();
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginRequestModel requestModel)
        {
            var loggedUser = _userService.Login(requestModel);

            ClaimsIdentity identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role,loggedUser.Role.ToString()),
                new Claim("StudentId",loggedUser.StudentId.ToString()),
                new Claim("NameSurname",loggedUser.NameSurname)
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
