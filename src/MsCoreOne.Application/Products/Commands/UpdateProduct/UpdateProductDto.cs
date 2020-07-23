using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductDto>
    {
        private readonly IApplicationDbContext _context;

        private readonly IStorageService _storageService;

        public UpdateProductCommandHandler(IApplicationDbContext context,
            IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<Unit> Handle(UpdateProductDto request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            entity.Name = request.Name;
            entity.Price = request.Price;
            entity.Description = request.Description;
            entity.BrandId = request.BrandId;
            entity.Rating = request.Rating;

            if (request.File != null)
            {
                entity.ImageFileName = await SaveFile(request.File);
            }

            var productCategory = entity.ProductCategories.FirstOrDefault();
            if (productCategory != null)
            {
                entity.ProductCategories.Remove(productCategory);
            }
            
            entity.ProductCategories.Add(new ProductCategory
            {
                ProductId = request.Id,
                CategoryId = request.CategoryId
            });

            await _context.SaveChangesAsync(cancellationToken);

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
