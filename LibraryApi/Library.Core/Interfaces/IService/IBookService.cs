using Library.Core.CustomEntities;
using Library.Core.Entities;
using Library.Core.QueryFilter;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.IService
{
    public interface IBookService
    {
        PagedList<Book> Gets(BookQueryFilter filters);
        Task<Book> Get(int id);
        Task Insert(Book item);
        void Update(Book item);
        Task<bool> Delete(int id);
    }
}
