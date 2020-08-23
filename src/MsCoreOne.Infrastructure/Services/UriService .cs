using Microsoft.AspNetCore.WebUtilities;
using MsCoreOne.Application.Common.Interfaces;
using MsCoreOne.Application.Common.Models;
using System;

namespace MsCoreOne.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            
            return new Uri(modifiedUri);
        }
    }
}
