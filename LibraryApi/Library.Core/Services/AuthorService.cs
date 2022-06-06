using Library.Core.CustomEntities;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Core.Interfaces.IService;
using Library.Core.QueryFilter;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOpcions _paginationOptions;
        public AuthorService(IUnitOfWork unitOfWork, IOptions<PaginationOpcions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Author> GetsAsync(AuthorQueryFilter filters)
        {

            var items = _unitOfWork.AuthorRepository.GetAuthors();
            if (filters.Id != null)
            {
                items = items.Where(f => f.Id == filters.Id).ToList();
            }
            if (filters.Ids != null)
            {
                items = items.Where(i => filters.Ids.Any(f => f == i.Id)).ToList();
            }
            if (filters.IdBook != null)
            {
                items = items.Where(f => f.IdBook == filters.IdBook).ToList();
            }
            if (filters.FirstName != null)
            {
                items = items.Where(f => f.FirstName.ToLower().Contains(filters.FirstName.ToLower())).ToList();
            }
            if (filters.LastName != null)
            {
                items = items.Where(f => f.LastName.ToLower().Contains(filters.LastName.ToLower())).ToList();
            }
            if (filters.SearchAll != null)
            {
                var search = filters.SearchAll.ToLower().Split(" ");
                items = items.Where(f =>
                        search.Any(p => (f.Id + "").Contains(p))
                        || search.Any(p => ((f.FirstName.ToLower()).Contains(p)))
                        || search.Any(p => ((f.LastName.ToLower()).Contains(p)))
                        || search.Any(p => ((f.IdBookNavigation.Title.ToLower()).Contains(p)))

            ).ToList();
            }
            switch (filters.OrderColumn)
            {
                case 0:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.Id).ToList() : items.OrderBy(o => o.Id).ToList();
                    break;
                case 1:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.FirstName).ToList() : items.OrderBy(o => o.FirstName).ToList();
                    break;
                case 2:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.LastName).ToList() : items.OrderBy(o => o.LastName).ToList();
                    break;
                case 3:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.IdBookNavigation.Title).ToList() : items.OrderBy(o => o.IdBookNavigation.Title).ToList();
                    break;
            }
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var paged = PagedList<Author>.Create(items, filters.PageNumber, filters.PageSize);
            return paged;
        }
        public async Task<Author> Get(int id)
        {
            return await _unitOfWork.AuthorRepository.GetById(id);
        }
        public async Task Insert(Author item)
        {
            await _unitOfWork.AuthorRepository.Add(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public void Update(Author item)
        {
            _unitOfWork.AuthorRepository.Update(item);
            _unitOfWork.SaveChange();
        }
        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.AuthorRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }


    }
}
