using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsCoreOne.Api.Controllers;

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
    }
}
