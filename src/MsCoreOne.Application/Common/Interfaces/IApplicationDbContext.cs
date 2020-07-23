using MsCoreOne.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<Brand> Brands { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
