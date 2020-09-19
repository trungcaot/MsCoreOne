using MediatR;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Exceptions;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryDto : IRequest
    {
        public int Id { get; set; }

        public DeleteCategoryDto(int id)
        {
            Id = id;
        }
    }

    public class DeleteCategoryCommandHandler : BaseHandler, IRequestHandler<DeleteCategoryDto>
    {
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public async Task<Unit> Handle(DeleteCategoryDto request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            _unitOfWork.Categories.Remove(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
