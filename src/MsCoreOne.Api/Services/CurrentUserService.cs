using MsCoreOne.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using IdentityModel;

namespace MsCoreOne.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Subject);

            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Name);
        }

        public string UserId { get; }

        public string Name { get; }
    }
}
