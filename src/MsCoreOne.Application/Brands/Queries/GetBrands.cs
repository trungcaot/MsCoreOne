using MediatR;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Brands.Queries
{
    public class GetBrandsQuery : IRequest<IEnumerable<Brand>>
    {
        public GetBrandsQuery() { }
    }

    public class GetCategoriesHandler : BaseHandler, IRequestHandler<GetBrandsQuery, IEnumerable<Brand>>
    {
        public GetCategoriesHandler(IUnitOfWork unitOfWork)
            :base(unitOfWork) { }

        public async Task<IEnumerable<Brand>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Brands.GetAllAsync();
        }
    }
}
