using Microsoft.EntityFrameworkCore;
using MsCoreOne.Application.Common.Interfaces.Repositories;
using MsCoreOne.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsCoreOne.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }
}
