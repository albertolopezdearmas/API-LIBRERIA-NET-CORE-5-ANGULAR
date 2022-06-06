using Library.Core.QueryFilter;
using Library.Infrastructure.Interfaces;
using System;

namespace Library.Infrastructure.Services
{
    public class UriServices : IUriServices
    {
        private readonly string _baseUri;

        public UriServices(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPaginationUri(QueryFilter query, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}" + "?PageSize=" + query.PageSize + "&PageNumber=" + query.PageNumber;
            return new Uri(baseUrl);
        }
    }
}
