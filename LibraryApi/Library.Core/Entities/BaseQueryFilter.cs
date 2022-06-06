namespace Library.Core.Entities
{
    public abstract class BaseQueryFilter
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int OrderColumn { get; set; } = 0;
        public string Order { get; set; } = "asc";
        public string SearchAll { get; set; }

    }
}
