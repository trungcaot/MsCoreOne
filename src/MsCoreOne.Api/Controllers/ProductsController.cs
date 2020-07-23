using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsCoreOne.Api.Controllers;
using MsCoreOne.Application.Products.Commands.CreateProduct;
using MsCoreOne.Application.Products.Commands.DeleteProduct;
using MsCoreOne.Application.Products.Commands.UpdateProduct;
using MsCoreOne.Application.Products.Queries;
using System.Threading.Tasks;

namespace MsCoreOne.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetProductsQuery()));
        }

        [HttpGet("categories/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            return Ok(await Mediator.Send(new GetProductsByCategoryIdQuery(categoryId)));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateProductDto dto)
        {
            return Ok(await Mediator.Send(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateProductDto dto)
        {
            return Ok(await Mediator.Send(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteProductDto(id)));
        }
    }
}
