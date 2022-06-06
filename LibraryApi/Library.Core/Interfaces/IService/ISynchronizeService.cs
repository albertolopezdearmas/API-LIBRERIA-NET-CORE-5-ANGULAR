using System.Threading.Tasks;

namespace Library.Core.Interfaces.IService
{
    public interface ISynchronizeService
    {
        Task<bool> GetSincronize(string url);
    }
}