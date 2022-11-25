using AutoMapper;

namespace KUSYS.Business.ObjectMappers
{
    public class CustomMappingProfiles : Profile
    {
        public CustomMappingProfiles()
        {
            IProfile invoiceProfile = new StudentMappingProfile();
        }
    }
}
