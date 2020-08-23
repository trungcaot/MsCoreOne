using MsCoreOne.Application.Common.Models;
using System;

namespace MsCoreOne.Application.Common.Interfaces
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
