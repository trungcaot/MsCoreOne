using MediatR;
using Microsoft.AspNetCore.Http;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Exceptions;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Products.Commands.UpdateProduct
{
    public partial class UpdateProductDto : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public IFormFile File { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }
    }

    public class UpdateProductCommandHandler : BaseHandler, IRequestHandler<UpdateProductDto>
    {
        private readonly IStorageService _storageService;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork,
            IStorageService storageService)
            :base(unitOfWork)
        {
            _storageService = storageService;
        }

        public async Task<Unit> Handle(UpdateProductDto request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.BrandId = request.BrandId;
            product.Rating = request.Rating;

            if (request.File != null)
            {
                product.ImageFileName = await SaveFile(request.File);
            }

            var productCategory = product.ProductCategories.FirstOrDefault();
            if (productCategory != null)
            {
                product.ProductCategories.Remove(productCategory);
            }

            //TODO: Need to enhance
            //product.ProductCategories.Add(new ProductCategory
            //{
            //    ProductId = request.Id,
            //    CategoryId = request.CategoryId
            //});

            _unitOfWork.Products.Update(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            
            return fileName;
        }
    }
}
