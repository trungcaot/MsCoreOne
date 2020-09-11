using MediatR;
using Microsoft.EntityFrameworkCore;
using MsCoreOne.Application.Categories.Queries.Dtos;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
        public GetCategoriesQuery() 
        {
        }
    }

    public class GetCategoriesHandler : BaseHandler, IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IMemoryCacheManager _memoryCacheManager;

        public GetCategoriesHandler(IApplicationDbContext context, 
            IMemoryCacheManager memoryCacheManager)
            :base(context)
        {
            _memoryCacheManager = memoryCacheManager;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            if (!_memoryCacheManager.TryGetValue(nameof(GetCategoriesQuery), out IEnumerable<CategoryDto> categoryDtos))
            {
                var categories = await _context.Categories.ToListAsync();

                categoryDtos = categories.Select(c => new CategoryDto
                                {
                                    Id = c.Id,
                                    Name = c.Name
                                });

                _memoryCacheManager.Set(nameof(GetCategoriesQuery), categoryDtos);
            }
            return categoryDtos;
        }
    }
}
