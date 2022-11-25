using AutoMapper;
using KUSYS.Business.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.WEB.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;
        public IConfiguration _configuration;

        public BaseController(IConfiguration configuration, IUnitOfWork uow, IMapper mapper)
        {
            _configuration = configuration;
            _uow = uow;
            _mapper = mapper;
        }

        protected TService GetService<TService>() where TService : class
        {
            return (TService)Activator.CreateInstance(typeof(TService), new object[] { _uow, _configuration, _mapper });
        }
    }
}
