using MediatR;
using Microsoft.EntityFrameworkCore;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Products.Queries.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Products.Queries
{
    public class GetProductsByCategoryIdQuery : IRequest<IEnumerable<ProductDto>>
    {
        public int CategoryId { get; set; }

        public GetProductsByCategoryIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }

    public class GetProductsByCategoryIdHandler : IRequestHandler<GetProductsByCategoryIdQuery, IEnumerable<ProductDto>>
    {
        private readonly IApplicationDbContext _context;

        private readonly IStorageService _storageService;

        public GetProductsByCategoryIdHandler(IApplicationDbContext context,
            IStorageService storageService)
        {
            _context = context;

            _storageService = storageService;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == request.CategoryId)).ToListAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Rating = p.Rating,
                ThumbnailImageUrl = _storageService.GetFileUrl(p.ImageFileName)
            }).ToList();
        }
    }
}
