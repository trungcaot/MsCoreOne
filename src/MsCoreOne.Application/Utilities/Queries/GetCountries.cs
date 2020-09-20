using MediatR;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Utilities.Queries.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Utilities.Queries
{
    public class GetCountriesQuery : IRequest<IEnumerable<CountryDto>>
    {
        public GetCountriesQuery() { }
    }

    public class GetCountriesHandler : BaseHandler, IRequestHandler<GetCountriesQuery, IEnumerable<CountryDto>>
    {
        private readonly IRedisCacheManager _redisCacheManager;

        public GetCountriesHandler(IUnitOfWork unitOfWork,
            IRedisCacheManager redisCacheManager)
            :base(unitOfWork)
        {
            _redisCacheManager = redisCacheManager;
        }

        public async Task<IEnumerable<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var countryDtos = new List<CountryDto>();

            countryDtos = await _redisCacheManager.GetAsync<List<CountryDto>>(nameof(GetCountriesQuery));
            if (countryDtos != null)
                return countryDtos;

            var countries = await _unitOfWork.Countries.GetAllAsync();
            countryDtos = countries.Select(c => new CountryDto
            {
                Id = c.Id,
                SortName = c.SortName,
                Name = c.Name
            }).ToList();

            await _redisCacheManager.SetAsync<List<CountryDto>>(nameof(GetCountriesQuery), countryDtos);
            return countryDtos;
        }
    }
}
