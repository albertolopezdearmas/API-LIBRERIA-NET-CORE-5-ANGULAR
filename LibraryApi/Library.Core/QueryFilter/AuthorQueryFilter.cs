using Library.Core.Entities;
using System;
using System.Collections.Generic;

namespace Library.Core.QueryFilter
{
    public class AuthorQueryFilter : BaseQueryFilter
    {
        public int? Id { get; set; }
        public int[] Ids { get; set; }
        public int? IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
