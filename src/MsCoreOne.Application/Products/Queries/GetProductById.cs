using MediatR;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Products.Queries.Dtos;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetProductByIdQueryHandler : BaseHandler, IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IStorageService _storageService;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork,
            IStorageService storageService)
            :base(unitOfWork)
        {
            _storageService = storageService;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Rating = product.Rating,
                ThumbnailImageUrl = _storageService.GetFileUrl(product.ImageFileName),
                BrandId = product.BrandId,
                CategoryId = product.ProductCategories.FirstOrDefault() == null ? 0 : product.ProductCategories.FirstOrDefault().CategoryId
            };
        }
    }
}
