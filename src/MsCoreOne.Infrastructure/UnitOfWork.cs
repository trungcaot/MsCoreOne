using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Common.Interfaces.Repositories;
using MsCoreOne.Infrastructure.Persistence;
using MsCoreOne.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private ICategoryRepository _categoryRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository Categories => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context));

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
