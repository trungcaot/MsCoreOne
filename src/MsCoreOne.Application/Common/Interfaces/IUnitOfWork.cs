using MsCoreOne.Application.Common.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }

        IBrandRepository Brands { get; }

        IProductRepository Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
