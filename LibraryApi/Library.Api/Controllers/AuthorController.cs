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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _iMapper;
        private readonly IUriServices _uriServices;
        public AuthorController(IAuthorService authorService, IMapper mapper, IUriServices uriServices)
        {
            _iMapper = mapper;
            _authorService = authorService;
            _uriServices = uriServices;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<AuthorBookDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<List<AuthorBookDto>>))]
        public IActionResult Get([FromQuery] AuthorQueryFilter filters)
        {
            var authors = _authorService.GetsAsync(filters);
            var authorsDto = _iMapper.Map<List<AuthorBookDto>>(authors);

            var metadata = new Metadata()
            {
                TotalCount = authors.TotalCount,
                TotalPage = authors.TotalPage,
                CurrentPage = authors.CurrentPage,
                PagesSize = authors.PagesSize,
                HasNextPage = authors.HasNextPage,
                HasPreviousPage = authors.HasPreviousPage,
                NextPageUrl = authors.HasNextPage ? _uriServices.GetPaginationUri(new QueryFilter() { PageNumber = filters.PageNumber + 1, PageSize = filters.PageSize }, Request.Path.Value).ToString() : null,
                PreviousPageUrl = authors.HasPreviousPage ? _uriServices.GetPaginationUri(new QueryFilter() { PageNumber = filters.PageNumber - 1, PageSize = filters.PageSize }, Request.Path.Value).ToString() : null,
            };
            var response = new ApiResponse<List<AuthorBookDto>>(authorsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("x-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<AuthorDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<AuthorDto>))]
        public async Task<IActionResult> GetId(int id)
        {
            var author = await _authorService.Get(id);
            var authorDto = _iMapper.Map<AuthorDto>(author);
            return Ok(new ApiResponse<AuthorDto>(authorDto));
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<AuthorDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<AuthorDto>))]
        public async Task<IActionResult> Post(AuthorDto authorDto)
        {
            Author author = _iMapper.Map<Author>(authorDto);
            await _authorService.Insert(author);
            return Ok(new ApiResponse<AuthorDto>(authorDto));
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Put(int id, AuthorDto authorDto)
        {
            authorDto.Id = id;
            Author author = _iMapper.Map<Author>(authorDto);
            _authorService.Update(author);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _authorService.Delete(id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
