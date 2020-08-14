using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsCoreOne.Application.Categories.Commands.CreateCategory;
using MsCoreOne.Application.Categories.Commands.DeleteCategory;
using MsCoreOne.Application.Categories.Commands.UpdateCategory;
using MsCoreOne.Application.Categories.Queries;
using MsCoreOne.Api.Controllers;
using System.Threading.Tasks;

namespace MsCoreOne.Controllers
{
    public class CategoriesController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetCategoriesQuery()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDto dto)
        {
            return Ok(await Mediator.Send(dto));
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryDto dto)
        {
            return Ok(await Mediator.Send(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryDto(id)));
        }
    }
}