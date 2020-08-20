using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MsCoreOne.Application.Categories.Queries.Dtos;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Common.Models;
using System;
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
        private readonly IMemoryCache _memoryCache;

        public GetCategoriesHandler(IApplicationDbContext context, IMemoryCache memoryCache)
            :base(context)
        {
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(nameof(GetCategoriesQuery), out IEnumerable<CategoryDto> categoryDtos))
            {
                var categories = await _context.Categories.ToListAsync();

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                categoryDtos = categories.Select(c => new CategoryDto
                                {
                                    Id = c.Id,
                                    Name = c.Name
                                });
                _memoryCache.Set(nameof(GetCategoriesQuery), categoryDtos, cacheExpiryOptions);
            }
            return categoryDtos;
        }
    }
}
