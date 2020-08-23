using MediatR;
using Microsoft.AspNetCore.Http;
using MsCoreOne.Application.Common.Helpers;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Common.Models;
using MsCoreOne.Application.Tests.Queries.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MsCoreOne.Application.Tests.Queries
{
    public class GetPagedDataQuery : IRequest<PagedResponse<IEnumerable<TestDto>>>
    {
        public PaginationFilter PaginationFilter { get; set; }

        public GetPagedDataQuery(PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }
    }

    public class GetPagedDataHandler : IRequestHandler<GetPagedDataQuery, PagedResponse<IEnumerable<TestDto>>>
    {
        private readonly IUriService _uriService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPagedDataHandler(IUriService uriService,
            IHttpContextAccessor httpContextAccessor)
        {
            _uriService = uriService;

            _httpContextAccessor = httpContextAccessor;
        }

        public Task<PagedResponse<IEnumerable<TestDto>>> Handle(GetPagedDataQuery request, CancellationToken cancellationToken)
        {
            var route = _httpContextAccessor.HttpContext.Request.Path.Value;

            var validFilter = new PaginationFilter(request.PaginationFilter.PageNumber, request.PaginationFilter.PageSize);

            var data = GetData();

            var pagedData = data.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize)
                                .ToList();
            var totalRecords = data.Count();

            var pagedReponse = PaginationHelper.CreatePagedReponse<TestDto>(pagedData, new PagedConfig(validFilter.PageNumber, validFilter.PageSize, totalRecords, route), _uriService);
            
            return Task.FromResult(pagedReponse);
        }

        private IEnumerable<TestDto> GetData()
        {
            var data = new List<TestDto>();
            for (int i = 1; i <= 30; i++)
            {
                var testDto = new TestDto(i, $"firstName {i}", $"lastName {i}");
                data.Add(testDto);
            }
            return data;
        }
    }
}
