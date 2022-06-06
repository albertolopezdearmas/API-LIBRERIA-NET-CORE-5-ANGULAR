using Library.Core.Interfaces;
using Library.Core.Interfaces.IRepository;
using Library.Infrastructure.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ISynchronizeRepository _synchronizeRepository;
        private readonly ISecurityRepository _securityRepository;
        public UnitOfWork(LibraryContext context)
        {
            _context = context;
        }

        public IAuthorRepository AuthorRepository => _authorRepository ?? new AuthorRepository(_context);
        public IBookRepository BookRepository => _bookRepository ?? new BookRepository(_context);
        public ISynchronizeRepository SynchronizeRepository => _synchronizeRepository ?? new SynchronizeRepository(new HttpClient());
        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(new HttpClient());

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
