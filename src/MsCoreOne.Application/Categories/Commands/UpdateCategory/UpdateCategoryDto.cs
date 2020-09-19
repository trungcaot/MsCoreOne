using MediatR;
using MsCoreOne.Application.Categories.Queries.Dtos;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Exceptions;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Categories.Commands.UpdateCategory
{
    public partial class UpdateCategoryDto : BaseDataConflict<CategoryDto>, IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UpdateCategoryCommandHandler : BaseHandler, IRequestHandler<UpdateCategoryDto>
    {
        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public async Task<Unit> Handle(UpdateCategoryDto request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            entity.Name = request.Name;

            _unitOfWork.Categories.Update(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
