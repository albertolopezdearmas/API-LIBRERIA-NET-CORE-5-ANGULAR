using Library.Core.QueryFilter;
using System;

namespace Library.Infrastructure.Interfaces
{
    public interface IUriServices
    {
        Uri GetPaginationUri(QueryFilter query, string actionUrl);
    }
}
