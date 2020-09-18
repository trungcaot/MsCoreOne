using MediatR;
using MsCoreOne.Application.Common.Bases;
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

    public class GetProductsByCategoryIdHandler : BaseHandler, IRequestHandler<GetProductsByCategoryIdQuery, IEnumerable<ProductDto>>
    {
        private readonly IStorageService _storageService;

        public GetProductsByCategoryIdHandler(IUnitOfWork unitOfWork,
            IStorageService storageService)
            :base(unitOfWork)
        {
            _storageService = storageService;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAllAsync();

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
