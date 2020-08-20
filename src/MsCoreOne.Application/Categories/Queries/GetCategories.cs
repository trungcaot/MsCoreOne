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
    public class GetCategoriesQuery : IRequest<PagedResponse<IEnumerable<CategoryDto>>>
    {
        public PaginationFilter PaginationFilter { get; set; }

        public GetCategoriesQuery(PaginationFilter paginationFilter) 
        {
            PaginationFilter = paginationFilter;
        }
    }

    public class GetCategoriesHandler : BaseHandler, IRequestHandler<GetCategoriesQuery, PagedResponse<IEnumerable<CategoryDto>>>
    {
        private readonly IMemoryCache _memoryCache;

        public GetCategoriesHandler(IApplicationDbContext context, IMemoryCache memoryCache)
            :base(context)
        {
            _memoryCache = memoryCache;
        }

        public async Task<PagedResponse<IEnumerable<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(request.PaginationFilter.PageNumber, request.PaginationFilter.PageSize);

            if (!_memoryCache.TryGetValue(nameof(GetCategoriesQuery), out IEnumerable<CategoryDto> categoryDtos))
            {
                var categories = await _context.Categories.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();

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
            return new PagedResponse<IEnumerable<CategoryDto>>(categoryDtos, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
