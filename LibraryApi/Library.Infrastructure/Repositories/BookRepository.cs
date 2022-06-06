using Library.Core.Entities;
using Library.Core.Interfaces.IRepository;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }

       
    }
}
