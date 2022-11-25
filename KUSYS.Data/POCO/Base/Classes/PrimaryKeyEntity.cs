using KUSYS.Data.POCO.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS.Data.POCO.Base.Classes
{
    public class PrimaryKeyEntity<T> : IPrimaryKey<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}
