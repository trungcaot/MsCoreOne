using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsCoreOne.Api.Controllers;
using MsCoreOne.Application.Common.Models;
using MsCoreOne.Application.Tests.Queries;
using System.Threading.Tasks;

namespace MsCoreOne.Controllers.ApiVersions.v2
{
    [ApiVersion("2.0")]
    public class DataController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public string Get()
        {
            return "Data from api v2";
        }

        [HttpGet("tests")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationFilter filter)
        {
            return Ok(await Mediator.Send(new GetPagedDataQuery(filter)).ConfigureAwait(false));
        }
    }
}
