using MediatR;
using Microsoft.EntityFrameworkCore;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Brands.Queries
{
    public class GetBrandsQuery : IRequest<IEnumerable<Brand>>
    {
        public GetBrandsQuery() { }
    }

    public class GetCategoriesHandler : IRequestHandler<GetBrandsQuery, IEnumerable<Brand>>
    {
        private readonly IApplicationDbContext _context;

        public GetCategoriesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Brands.ToListAsync();
        }
    }
}
