using AutoMapper;
using KUSYS.Business.UnitOfWorks;
using Microsoft.Extensions.Configuration;

namespace KUSYS.Business.Services.Base
{
    public class BaseService
    {
        protected IConfiguration _configuration;
        protected IUnitOfWork _uow;
        protected IMapper _mapper;
        protected BaseService(IConfiguration configuration, IUnitOfWork uow, IMapper mapper)
        {
            _configuration = configuration;
            _uow = uow;
            _mapper = mapper;
        }
    }
}
