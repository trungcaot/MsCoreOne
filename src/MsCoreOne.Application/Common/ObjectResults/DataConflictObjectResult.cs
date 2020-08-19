using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MsCoreOne.Application.Common.ObjectResults
{
    public class DataConflictObjectResult : ObjectResult
    {
        public DataConflictObjectResult(object value)
            :base(value)
        {
            StatusCode = (int)HttpStatusCode.Conflict;
        }
    }
}
