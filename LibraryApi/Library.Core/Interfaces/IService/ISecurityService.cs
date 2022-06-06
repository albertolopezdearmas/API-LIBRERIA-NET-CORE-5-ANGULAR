using Library.Core.Entities;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.IService
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin, string url);
    }
}
