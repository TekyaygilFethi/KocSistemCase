using KUSYS.Data.POCO.Base.Interfaces;

namespace KUSYS.Data.POCO.Base.Classes
{
    public class BasePOCOEntity<T> : PrimaryKeyEntity<T>, IModifiable
    {
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
