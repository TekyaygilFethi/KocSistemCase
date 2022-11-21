using KUSYS.Data.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Data.Business.JWT
{
    public class JWTUserClaimModel
    {
        public Guid UserId { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
