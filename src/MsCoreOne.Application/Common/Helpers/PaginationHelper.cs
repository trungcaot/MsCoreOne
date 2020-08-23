using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Common.Models;
using System;
using System.Collections.Generic;

namespace MsCoreOne.Application.Common.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResponse<IEnumerable<T>> CreatePagedReponse<T>(List<T> pagedData, PagedConfig pagedConfig, IUriService uriService)
        {
            var respose = new PagedResponse<IEnumerable<T>>(pagedData, pagedConfig.PageNumber, pagedConfig.PageSize);
            
            var totalPages = ((double)pagedConfig.TotalRecords / (double)pagedConfig.PageSize);
            
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            respose.NextPage = GetNextPageUri(uriService, pagedConfig, roundedTotalPages);
            respose.PreviousPage = GetPreviousPageUri(uriService, pagedConfig, roundedTotalPages);
            
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, pagedConfig.PageSize), pagedConfig.Route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, pagedConfig.PageSize), pagedConfig.Route);
            
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = pagedConfig.TotalRecords;
            
            return respose;
        }

        private static Uri GetNextPageUri(IUriService uriService, PagedConfig pagedConfig, int roundedTotalPages)
        {
            return pagedConfig.PageNumber >= 1 && pagedConfig.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(pagedConfig.PageNumber + 1, pagedConfig.PageSize), pagedConfig.Route)
                : null;
        }

        private static Uri GetPreviousPageUri(IUriService uriService, PagedConfig pagedConfig, int roundedTotalPages)
        {
            return pagedConfig.PageNumber - 1 >= 1 && pagedConfig.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(pagedConfig.PageNumber - 1, pagedConfig.PageSize), pagedConfig.Route)
                : null;
        }
    }
}
