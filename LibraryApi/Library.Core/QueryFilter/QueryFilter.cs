using Library.Core.Interfaces;

namespace Library.Core.QueryFilter
{
    public class QueryFilter : IQueryFilter
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}
