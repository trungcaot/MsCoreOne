using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsCoreOne.Api.Controllers;

namespace MsCoreOne.Controllers.ApiVersions.v1
{
    [ApiVersion("1.0")]
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
