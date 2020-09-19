using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MsCoreOne.Application.Common.Bases;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Utilities.Queries.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly IDistributedCache _distributedCache;

        public GetCountriesHandler(IUnitOfWork unitOfWork,
            IDistributedCache distributedCache)
            :base(unitOfWork)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IEnumerable<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var countryDtos = new List<CountryDto>();
            var cacheKey = nameof(GetCountriesQuery);
            string serializedCountries;

            var redisCountries = await _distributedCache.GetAsync(cacheKey);
            if (redisCountries != null)
            {
                serializedCountries = Encoding.UTF8.GetString(redisCountries);
                countryDtos = JsonConvert.DeserializeObject<List<CountryDto>>(serializedCountries);
            }
            else
            {
                var countries = await _unitOfWork.Countries.GetAllAsync();
                countryDtos = countries.Select(c => new CountryDto
                {
                    Id = c.Id,
                    SortName = c.SortName,
                    Name = c.Name
                }).ToList();

                serializedCountries = JsonConvert.SerializeObject(countryDtos);
                redisCountries = Encoding.UTF8.GetBytes(serializedCountries);

                //TODO: Need to move options to global of the system.
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisCountries, options);
            }
            return countryDtos;
        }
    }
}
