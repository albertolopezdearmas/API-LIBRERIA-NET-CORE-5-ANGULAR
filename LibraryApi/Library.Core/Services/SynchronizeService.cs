using Library.Core.Interfaces;
using Library.Core.Interfaces.IService;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class SynchronizeService : ISynchronizeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SynchronizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> GetSincronize(string url)
        {
            var authors = await _unitOfWork.SynchronizeRepository.GetAuthorApi(url + "/api/v1/Authors");
            var books = await _unitOfWork.SynchronizeRepository.GetBookApi(url + "/api/v1/books");
            var retultBookDelete = await _unitOfWork.BookRepository.DeleteAll();
            var resultAuthorDelete = await _unitOfWork.AuthorRepository.DeleteAll();
            if (retultBookDelete && resultAuthorDelete)
                if (await _unitOfWork.BookRepository.AddAll(books) && await _unitOfWork.AuthorRepository.AddAll(authors))
                    return true;
            return false;
        }

    }
}
