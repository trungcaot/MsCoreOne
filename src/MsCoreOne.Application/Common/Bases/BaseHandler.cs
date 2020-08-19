using MsCoreOne.Application.Common.Interfaces;

namespace MsCoreOne.Application.Common.Bases
{
    public abstract class BaseHandler
    {
        protected readonly IApplicationDbContext _context;

        protected BaseHandler(IApplicationDbContext context)
        {
            _context = context;
        }
    }
}
