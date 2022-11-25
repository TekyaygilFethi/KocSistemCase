using AutoMapper;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Web.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;

namespace KUSYS.WEBUI.Controllers.Base
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


        // Servis Interface'inden nesnesi türetiliyor
        protected TService GetService<TService>() where TService : class
        {
            string className = typeof(TService).Name.Substring(1);
            var namespacePath = GLOBALS.NAMESPACE_PATH+".Business";
            Assembly assembly = Assembly.Load(namespacePath);
            var t = assembly.GetType(namespacePath + ".Services.Classes." + $"{className}");

            return t == default ? default : (TService)Activator.CreateInstance(t, new object[] { _uow, _configuration, _mapper });
        }
    }
}
