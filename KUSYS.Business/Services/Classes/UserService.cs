using AutoMapper;
using KUSYS.Business.Repositories;
using KUSYS.Business.Services.Base;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.Services.UserService;
using KUSYS.Data.Exceptions;
using KUSYS.Data.POCO;
using KUSYS.Helper.WebHelpers;
using Microsoft.Extensions.Configuration;

namespace KUSYS.Business.Services.Classes
{
    public class UserService : BaseService, IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IUnitOfWork uow, IConfiguration configuration, IMapper mapper) : base(configuration, uow, mapper)
        {
            _userRepository = uow.GetRepository<User>();
        }

        public LoginResponseModel Login(LoginRequestModel request)
        {
            if (request == null) throw new AppException("Login Request Model can not be null!");

            string salt = _configuration.GetSection("Salt").Value;

            var user = _userRepository.Single(a => a.Username == request.Username && a.Password == CryptographyHelper.Encode(request.Password + salt), i => i.Student);
            if (user == null) throw new AppException("Wrong credentials!");

            return new LoginResponseModel
            {
                Role = user.Role,
                StudentId = user.Student.Id,
                NameSurname = user.Student.FirstName + " " + user.Student.Lastname
            };
        }
    }
}
