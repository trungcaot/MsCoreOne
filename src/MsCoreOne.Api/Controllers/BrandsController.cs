using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsCoreOne.Api.Controllers;
using MsCoreOne.Application.Brands.Queries;
using System.Threading.Tasks;

namespace MsCoreOne.Controllers
{
    public class BrandsController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetBrandsQuery()));
        }
    }
}
