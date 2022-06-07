using Library.Core.CustomEntities;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Core.Interfaces.IService;
using Library.Core.QueryFilter;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOpcions _paginationOptions;
        public BookService(IUnitOfWork unitOfWork, IOptions<PaginationOpcions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Book> Gets(BookQueryFilter filters)
        {

            var items = _unitOfWork.BookRepository.GetAll();
            if (filters.Id != null)
            {
                items = items.Where(f => f.Id == filters.Id).ToList();
            }
            if (filters.Title != null)
            {
                items = items.Where(f => f.Title.ToLower().Contains(filters.Title.ToLower())).ToList();
            }
            if (filters.Description != null)
            {
                items = items.Where(f => f.Description.ToLower().Contains(filters.Description.ToLower())).ToList();
            }
            if (filters.Excerpt != null)
            {
                items = items.Where(f => f.Excerpt.ToLower().Contains(filters.Excerpt.ToLower())).ToList();
            }
            if (filters.PublishDate != null)
            {
                items = items.Where(f => f.PublishDate.ToShortDateString() == filters.PublishDate.Value.ToShortDateString()).ToList();
            }
            if (filters.StrarPublishDate != null && filters.EndPublishDate!=null)
            {
                items = items.Where(f => f.PublishDate <= filters.EndPublishDate.Value.AddDays(1)  &&
                f.PublishDate>= filters.StrarPublishDate.Value).ToList();
            }
            if (filters.PageCount != null)
            {
                items = items.Where(f => f.PageCount == filters.PageCount).ToList();
            }
            if (filters.SearchAll != null)
            {
                var search = filters.SearchAll.ToLower().Split(" ");
                items = items.Where(f =>
                        search.Any(p => (f.PageCount + "").Contains(p))
                        || search.Any(p => (f.PublishDate.ToString("dd/MM/yyyy hh:mm:ss").Contains(p)))
                        || search.Any(p => ((f.Excerpt.ToLower()).Contains(p)))
                        || search.Any(p => ((f.Description.ToLower()).Contains(p)))
                        || search.Any(p => ((f.Title.ToLower()).Contains(p)))
                        || search.Any(p => ((f.PageCount + "").Contains(p)))
                        || search.Any(p => ((f.Id + "").Contains(p)))
            ).ToList();
            }
            switch (filters.OrderColumn)
            {
                case 0:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.Id).ToList() : items.OrderBy(o => o.Id).ToList();
                    break;
                case 1:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.Title).ToList() : items.OrderBy(o => o.Title).ToList();
                    break;
                case 2:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.Description).ToList() : items.OrderBy(o => o.Description).ToList();
                    break;
                case 3:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.PageCount).ToList() : items.OrderBy(o => o.PageCount).ToList();
                    break;
                case 4:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.Excerpt).ToList() : items.OrderBy(o => o.Excerpt).ToList();
                    break;
                case 5:
                    items = filters.Order == "desc" ? items.OrderByDescending(o => o.PublishDate).ToList() : items.OrderBy(o => o.PublishDate).ToList();
                    break;
            }


            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = (int)(filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : (filters.PageSize==-1?items.Count: filters.PageSize));
            var paged = PagedList<Book>.Create(items, filters.PageNumber, filters.PageSize);
            return paged;
        }
        public async Task<Book> Get(int id)
        {
            return await _unitOfWork.BookRepository.GetById(id);
        }
        public async Task Insert(Book item)
        {
            await _unitOfWork.BookRepository.Add(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public void Update(Book item)
        {
            _unitOfWork.BookRepository.Update(item);
            _unitOfWork.SaveChange();
        }
        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.BookRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }


    }
}
