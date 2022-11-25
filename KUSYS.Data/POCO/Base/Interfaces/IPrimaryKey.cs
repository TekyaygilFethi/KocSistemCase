namespace KUSYS.Data.POCO.Base.Interfaces
{
    public interface IPrimaryKey<T>
    {
        public T Id { get; set; }
    }
}
