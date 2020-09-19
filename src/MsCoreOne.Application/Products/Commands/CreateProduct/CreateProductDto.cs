using MediatR;
using Microsoft.AspNetCore.Http;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Products.Commands.CreateProduct
{
    public class CreateProductDto : IRequest<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public IFormFile File { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }
    }

    public class CreateProductCommandHandler : BaseHandler, IRequestHandler<CreateProductDto, int>
    {
        private readonly IStorageService _storageService;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork,
            IStorageService storageService)
            :base(unitOfWork)
        {
            _storageService = storageService;
        }

        public async Task<int> Handle(CreateProductDto request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Id == request.CategoryId);

            var product = new Product
            { 
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Rating = request.Rating,
                BrandId = request.BrandId
            };

            if (request.File != null)
            {
                product.ImageFileName = await SaveFile(request.File);
            }

            product.ProductCategories = new List<ProductCategory>
            {
                new ProductCategory
                {
                    Product = product,
                    Category = category
                }
            };
            
            await _unitOfWork.Products.Add(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;
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
