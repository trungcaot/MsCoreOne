using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Application.Common.Models
{
    public class PagedConfig
    {
        public PagedConfig()
        {
        }

        public PagedConfig(int pageNumber, int pageSize, int total, string route)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = total;
            Route = route;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public string Route { get; set; }
    }
}
