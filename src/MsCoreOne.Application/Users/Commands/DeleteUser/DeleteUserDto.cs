using MediatR;
using MsCoreOne.Application.Common.Exceptions;
using MsCoreOne.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Users.Commands.DeleteUser
{
    public class DeleteUserDto : IRequest
    {
        public Guid Id { get; set; }

        public DeleteUserDto(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserDto>
    {
        private readonly IIdentityService _identityService;

        public DeleteUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(DeleteUserDto request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserNameAsync(request.Id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(DeleteUserDto), request.Id);
            }

            await _identityService.DeleteUserAsync(request.Id.ToString());

            return Unit.Value;
        }
    }
}
