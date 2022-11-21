using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
