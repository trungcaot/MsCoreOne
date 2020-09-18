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
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public GetProductsQuery() { }
    }

    public class GetProductsHandler : BaseHandler, IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IStorageService _storageService;

        public GetProductsHandler(IUnitOfWork unitOfWork,
            IStorageService storageService)
            :base(unitOfWork)
        {
            _storageService = storageService;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Rating = p.Rating,
                ThumbnailImageUrl = _storageService.GetFileUrl(p.ImageFileName),
                BrandId = p.BrandId,
                CategoryId = p.ProductCategories.FirstOrDefault() == null ? 0 : p.ProductCategories.FirstOrDefault().CategoryId
            }).ToList();
        }
    }
}
