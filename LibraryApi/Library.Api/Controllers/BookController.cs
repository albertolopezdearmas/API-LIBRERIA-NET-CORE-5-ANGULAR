using AutoMapper;
using Library.Api.Response;
using Library.Core.CustomEntities;
using Library.Core.DTOs;
using Library.Core.Entities;
using Library.Core.Interfaces.IService;
using Library.Core.QueryFilter;
using Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _iMapper; private readonly IUriServices _uriServices;
        public BookController(IBookService bookService, IMapper mapper, IUriServices uriServices)
        {
            _iMapper = mapper;
            _bookService = bookService;
            _uriServices = uriServices;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BookDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<BookDto>>))]
        public IActionResult Get([FromQuery] BookQueryFilter filters)
        {
            var books = _bookService.Gets(filters);
            var booksDto = _iMapper.Map<IEnumerable<BookDto>>(books);

            var metadata = new Metadata()
            {
                TotalCount = books.TotalCount,
                TotalPage = books.TotalPage,
                CurrentPage = books.CurrentPage,
                PagesSize = books.PagesSize,
                HasNextPage = books.HasNextPage,
                HasPreviousPage = books.HasPreviousPage,
                NextPageUrl = books.HasNextPage ? _uriServices.GetPaginationUri(new QueryFilter() { PageNumber = filters.PageNumber + 1, PageSize = filters.PageSize }, Request.Path.Value).ToString() : null,
                PreviousPageUrl = books.HasPreviousPage ? _uriServices.GetPaginationUri(new QueryFilter() { PageNumber = filters.PageNumber - 1, PageSize = filters.PageSize }, Request.Path.Value).ToString() : null,
            };
            var response = new ApiResponse<IEnumerable<BookDto>>(booksDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("x-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<BookDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<BookDto>))]
        public async Task<IActionResult> GetId(int id)
        {
            var book = await _bookService.Get(id);
            var bookDto = _iMapper.Map<BookDto>(book);
            return Ok(new ApiResponse<BookDto>(bookDto));
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<BookDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<BookDto>))]
        public async Task<IActionResult> Post(BookDto bookDto)
        {
            Book book = _iMapper.Map<Book>(bookDto);
            await _bookService.Insert(book);
            return Ok(new ApiResponse<BookDto>(bookDto));
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Put(int id, BookDto bookDto)
        {
            bookDto.Id = id;
            Book book = _iMapper.Map<Book>(bookDto);
            _bookService.Update(book);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.Delete(id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
