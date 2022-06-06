using Library.Core.Entities;
using System;

namespace Library.Core.QueryFilter
{
    public class BookQueryFilter : BaseQueryFilter
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Excerpt { get; set; }
        public int? PageCount { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? StrarPublishDate { get; set; }
        public DateTime? EndPublishDate { get; set; }
    }
}
