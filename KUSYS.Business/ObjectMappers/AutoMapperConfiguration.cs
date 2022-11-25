using AutoMapper;

namespace KUSYS.Business.ObjectMappers
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StudentMappingProfile>();
            });
            return config;
        }
    }
}
