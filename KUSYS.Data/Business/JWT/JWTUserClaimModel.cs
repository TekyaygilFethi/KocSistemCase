using KUSYS.Data.POCO;

namespace KUSYS.Data.Business.JWT
{
    public class JWTUserClaimModel
    {
        public Guid UserId { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
