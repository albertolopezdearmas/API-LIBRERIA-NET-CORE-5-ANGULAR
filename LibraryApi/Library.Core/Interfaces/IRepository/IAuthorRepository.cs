using Library.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.IRepository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        List<Author> GetAuthors();
    }
    
}
