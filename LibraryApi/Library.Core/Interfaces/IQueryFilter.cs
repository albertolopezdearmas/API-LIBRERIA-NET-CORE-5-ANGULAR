namespace Library.Core.Interfaces
{
    public interface IQueryFilter
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
