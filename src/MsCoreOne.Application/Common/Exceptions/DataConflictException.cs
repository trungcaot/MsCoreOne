using MsCoreOne.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Application.Common.Exceptions
{
    public class DataConflictException : Exception
    {
        public IDictionary<string, object> Failures { get; }

        public DataConflictException(IList<Difference> fields)
        {
            Failures = new Dictionary<string, object>
            {
                { "fields", fields }
            };
        }
    }
}
