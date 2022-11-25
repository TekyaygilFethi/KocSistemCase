using KUSYS.Data.POCO.Base.Interfaces;

namespace KUSYS.Data.POCO.Base.Classes
{
    public class SoftDeletableBasePOCOEntity<T> : BasePOCOEntity<T>, ISoftDeletable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
