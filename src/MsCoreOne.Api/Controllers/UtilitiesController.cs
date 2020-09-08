using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsCoreOne.Api.Controllers;
using MsCoreOne.Application.Utilities.Queries;
using System.Threading.Tasks;

namespace MsCoreOne.Controllers
{
    public class UtilitiesController : ApiController
    {
        [HttpGet("countries")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await Mediator.Send(new GetCountriesQuery()).ConfigureAwait(false));
        }
    }
}
