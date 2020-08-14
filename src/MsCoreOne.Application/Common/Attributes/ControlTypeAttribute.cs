using MsCoreOne.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ControlTypeAttribute : Attribute
    {
        public ControlTypeEnum ControlType { get; set; }

        public ControlTypeAttribute(ControlTypeEnum controlType)
        {
            ControlType = controlType;
        }
    }
}
