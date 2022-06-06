using Library.Core.Entities;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.IRepository
{
    public interface ISecurityRepository 
    {
        Task<Security> GetLoginByCredentials(string path, UserLogin login);
    }
}
