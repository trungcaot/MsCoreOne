using MediatR;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryDto : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateCategoryCommandHandler : BaseHandler, IRequestHandler<CreateCategoryDto, int>
    {
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public async Task<int> Handle(CreateCategoryDto request, CancellationToken cancellationToken)
        {
            var category = new Category { Name = request.Name };

            await _unitOfWork.Categories.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
