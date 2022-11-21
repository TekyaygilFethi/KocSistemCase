using AutoMapper;
using KUSYS.Business.Repositories;
using KUSYS.Business.Services.Base;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.JWT;
using KUSYS.Data.Business.Services.UserService;
using KUSYS.Data.Exceptions;
using KUSYS.Data.POCO;
using KUSYS.Helper.WebHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business.Services.Classes
{
    public class UserService : BaseService, IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IUnitOfWork uow, IConfiguration configuration, IMapper mapper) : base(configuration,uow, mapper)
        {
            _userRepository = uow.GetRepository<User>();
        }

        public string Login(LoginRequestModel request)
        {
            if (request == null) throw new AppException("Login Request Model can not be null!");
         
            string salt = _configuration.GetSection("Salt").Value;
            
            var user = _userRepository.Single(a=>a.Username == request.Username && a.Password == CryptographyHelper.Encode(request.Password + salt));
            if (user == null) throw new AppException("Wrong credentials!");

           JWTUserClaimModel a = new JWTUserClaimModel
            {
                UserId = user.Id,
                Role = user.Role
            };

            var token = JWTHelper.GetJwtToken(user.Id.ToString(), _configuration, new TimeSpan(2, 0, 0), new Claim[] { new Claim("Role", user.Role.ToString()) });

            return token;
        }
    }
}
