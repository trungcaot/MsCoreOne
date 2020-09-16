using MsCoreOne.Application.Common.Interfaces;

namespace MsCoreOne.Application.Common.Bases
{
    public abstract class BaseHandler
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
