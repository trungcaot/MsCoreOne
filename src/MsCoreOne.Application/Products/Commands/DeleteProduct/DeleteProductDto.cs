using MediatR;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Exceptions;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductDto : IRequest
    {
        public int Id { get; set; }

        public DeleteProductDto(int id)
        {
            Id = id;
        }
    }

    public class DeleteProductCommandHandler : BaseHandler, IRequestHandler<DeleteProductDto>
    {
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public async Task<Unit> Handle(DeleteProductDto request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _unitOfWork.Products.Remove(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
