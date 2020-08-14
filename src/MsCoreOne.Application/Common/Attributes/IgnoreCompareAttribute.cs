using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IgnoreCompareAttribute : Attribute
    {
    }
}
