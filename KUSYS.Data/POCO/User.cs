using KUSYS.Data.POCO.Base.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS.Data.POCO
{
    [Table("Users")]
    public class User : BasePOCOEntity<Guid>
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Student Student { get; set; }

        public UserRoleEnum Role { get; set; }
    }
}
