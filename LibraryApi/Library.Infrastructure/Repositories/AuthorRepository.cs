using Library.Core.Entities;
using Library.Core.Interfaces.IRepository;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context) : base(context)
        {

        }
        public List<Author> GetAuthors()
        {
            return _entities.Include(a => a.IdBookNavigation).ToList();
        }

    }
}
