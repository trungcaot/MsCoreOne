using Microsoft.AspNetCore.Mvc;
using MsCoreOne.Application.Users.Commands.CreateUser;
using MsCoreOne.Application.Users.Queries;
using MsCoreOne.Api.Controllers;
using System.Threading.Tasks;
using System;
using MsCoreOne.Application.Users.Commands.DeleteUser;
using MsCoreOne.Application.Users.Commands.UpdateUser;
using Microsoft.AspNetCore.Authorization;

namespace MsCoreOne.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
        {
            return Ok(await Mediator.Send(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserDto dto)
        {
            return Ok(await Mediator.Send(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteUserDto(id)));
        }
    }
}
