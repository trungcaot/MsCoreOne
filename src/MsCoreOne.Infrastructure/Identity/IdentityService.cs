using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MsCoreOne.Application.Users.Share;
using System.Collections;
using System.Collections.Generic;
using MsCoreOne.Application.Users.Commands.CreateUser;
using System;
using MsCoreOne.Application.Users.Commands.UpdateUser;

namespace MsCoreOne.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            return users.Where(u => u.UserName.ToLower() != "admin@mscoreone.com").Select(u => new UserDto
            {
                FirstName = u.FirstName,
                LastName = u.Lastname,
                UserName = u.UserName,
                Id = u.Id
            });
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(CreateUserDto userDto)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = userDto.FirstName,
                Lastname = userDto.LastName,
                UserName = userDto.UserName
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<(Result Result, string UserId)> UpdateUserAsync(UpdateUserDto userDto)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userDto.Id);

            if (user != null)
            {
                user.FirstName = userDto.FirstName;
                user.Lastname = userDto.LastName;

                return await UpdateUserAsync(user);
            }

            return (Result.Success(), user.Id);
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        private async Task<(Result Result, string UserId)> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            return (result.ToApplicationResult(), user.Id);
        }

        private async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        
    }
}
