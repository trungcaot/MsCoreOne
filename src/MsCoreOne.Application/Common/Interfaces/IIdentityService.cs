using MsCoreOne.Application.Common.Models;
using MsCoreOne.Application.Users.Commands.CreateUser;
using MsCoreOne.Application.Users.Commands.UpdateUser;
using MsCoreOne.Application.Users.Share;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(CreateUserDto userDto);

        Task<(Result Result, string UserId)> UpdateUserAsync(UpdateUserDto userDto);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
