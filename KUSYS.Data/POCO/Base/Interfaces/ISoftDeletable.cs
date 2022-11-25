namespace KUSYS.Data.POCO.Base.Interfaces
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
