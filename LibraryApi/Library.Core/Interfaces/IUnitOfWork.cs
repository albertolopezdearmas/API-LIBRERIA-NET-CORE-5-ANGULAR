using Library.Core.Interfaces.IRepository;
using System;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        ISynchronizeRepository SynchronizeRepository { get; }
        ISecurityRepository SecurityRepository { get; }

        void SaveChange();
        Task SaveChangesAsync();
    }
}
