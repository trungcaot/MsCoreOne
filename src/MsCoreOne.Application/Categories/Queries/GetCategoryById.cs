using MediatR;
using MsCoreOne.Application.Categories.Queries.Dtos;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }

        public GetCategoryByIdQuery(int id) 
        {
            Id = id;
        }
    }

    public class GetCategoryByIdQueryHandler : BaseHandler, IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        public GetCategoryByIdQueryHandler(IApplicationDbContext context)
            :base(context) { }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            return new CategoryDto(category.Id, category.Name);
        }
    }
}
