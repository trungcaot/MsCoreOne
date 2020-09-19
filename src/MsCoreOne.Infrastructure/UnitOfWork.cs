using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Common.Interfaces.Repositories;
using MsCoreOne.Infrastructure.Persistence;
using MsCoreOne.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private ICategoryRepository _categoryRepository;
        private IBrandRepository _brandRepository;
        private IProductRepository _productRepository;
        private ICountryRepository _countryRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository Categories => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context));
        public IBrandRepository Brands => _brandRepository ?? (_brandRepository = new BrandRepository(_context));
        public IProductRepository Products => _productRepository ?? (_productRepository = new ProductRepository(_context));
        public ICountryRepository Countries => _countryRepository ?? (_countryRepository = new CountryRepository(_context));
        

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
