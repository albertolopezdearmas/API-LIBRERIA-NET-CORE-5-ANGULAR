using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Core.Interfaces.IService;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin, string url)
        {
            return await _unitOfWork.SecurityRepository.GetLoginByCredentials(url + "/api/v1/Users", userLogin);
        }

       
    }
}
