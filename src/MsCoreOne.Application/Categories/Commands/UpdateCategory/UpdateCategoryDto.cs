using MediatR;
using MsCoreOne.Application.Categories.Queries.Dtos;
using MsCoreOne.Application.Common.Base;
using MsCoreOne.Application.Common.Exceptions;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Categories.Commands.UpdateCategory
{
    public partial class UpdateCategoryDto : DataConflictBase<CategoryDto>, IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryDto request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
