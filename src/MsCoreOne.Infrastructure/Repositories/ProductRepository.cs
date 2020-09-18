using MsCoreOne.Application.Common.Interfaces.Repositories;
using MsCoreOne.Domain.Entities;
using MsCoreOne.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
           : base(context) { }
    }
}
