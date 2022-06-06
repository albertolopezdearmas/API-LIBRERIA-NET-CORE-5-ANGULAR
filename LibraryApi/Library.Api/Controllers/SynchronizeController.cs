using Library.Api.Response;
using Library.Core.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SynchronizeController : ControllerBase
    {
        private readonly ISynchronizeService _synchronizeService;
        private readonly IConfiguration _configuration;
        public SynchronizeController(IConfiguration configuration, ISynchronizeService SynchronizeService)
        {
            _configuration = configuration;
            _synchronizeService = SynchronizeService;
        }


        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _synchronizeService.GetSincronize(_configuration["UriApiData"]);
            return Ok(new ApiResponse<bool>(result));
        }


    }
}
