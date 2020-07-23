using MediatR;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Users.Share;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Users.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public GetUsersQuery() { }
    }

    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IIdentityService _identityService;

        public GetUsersHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetUsers();
        }
    }
}
