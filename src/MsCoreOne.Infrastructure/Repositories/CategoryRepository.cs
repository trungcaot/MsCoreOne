using MsCoreOne.Application.Common.Interfaces.Repositories;
using MsCoreOne.Domain.Entities;
using MsCoreOne.Infrastructure.Persistence;

namespace MsCoreOne.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            :base(context) { }
    }
}
