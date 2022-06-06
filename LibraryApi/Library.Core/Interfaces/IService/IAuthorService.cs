using Library.Core.CustomEntities;
using Library.Core.Entities;
using Library.Core.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.IService
{
    public interface IAuthorService
    {
        PagedList<Author> GetsAsync(AuthorQueryFilter filters);
        Task<Author> Get(int id);
        Task Insert(Author item);
        void Update(Author item);
        Task<bool> Delete(int id);
    }
}
