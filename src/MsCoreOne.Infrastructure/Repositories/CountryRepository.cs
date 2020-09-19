using MsCoreOne.Application.Common.Interfaces.Repositories;
using MsCoreOne.Domain.Entities;
using MsCoreOne.Infrastructure.Persistence;

namespace MsCoreOne.Infrastructure.Repositories
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context)
            : base(context) { }
    }
}
