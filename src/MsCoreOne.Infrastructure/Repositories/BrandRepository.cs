using MsCoreOne.Application.Common.Interfaces.Repositories;
using MsCoreOne.Domain.Entities;
using MsCoreOne.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Infrastructure.Repositories
{
    class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context)
            : base(context) { }
    }
}
