using MsCoreOne.Application.Common.Attributes;
using MsCoreOne.Application.Common.Enums;
using MsCoreOne.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static MsCoreOne.Application.Common.Constants;

namespace MsCoreOne.Application.Common.Extensions
{
    public static class CompareObjectsExtension
    {
        public static IEnumerable<Difference> Compare(this object obj, object another)
        {
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && !p.GetCustomAttributes(true).Any(a => a is IgnoreCompareAttribute)).ToList();

            foreach (var propertyInfo in properties)
            {
                var objValue = propertyInfo.GetValue(obj, null);
                var anotherValue = propertyInfo.GetValue(another, null);

                if (!IsComparable(propertyInfo.PropertyType) || AreEqual(objValue, anotherValue))
                {
                    continue;
                }
                if (Attribute.IsDefined(propertyInfo, typeof(ControlTypeAttribute)))
                {
                    objValue = TransformValue(propertyInfo, objValue);
                }
                yield return new Difference(propertyInfo.Name.ToCamelCase(), objValue.ToString());
            }
        }

        public static bool IsDefault<T>(this T obj)
        {
            return EqualityComparer<T>.Default.Equals(obj, default);
        }

        private static string TransformValue(PropertyInfo propertyInfo, object objValue)
        {
            var controlType = propertyInfo.GetCustomAttribute<ControlTypeAttribute>().ControlType;
            switch (controlType)
            {
                case ControlTypeEnum.CheckBox:
                    return objValue.ToString() == "True" ? DataConflict.Checked : DataConflict.UnChecked;
                case ControlTypeEnum.Toggle:
                    return objValue.ToString() == "True" ? DataConflict.On : DataConflict.Off;
                case ControlTypeEnum.Currency:
                    return $"{ControlTypeEnum.Currency.ToString().ToCamelCase()}|{objValue}";
                default:
                    return objValue.ToString();
            }
        }

        private static bool IsComparable(this Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) ||
                   type.IsPrimitive ||
                   type.IsValueType;
        }

        private static bool AreEqual(object value1, object value2)
        {
            if (value1 == null && value2 != null || value1 != null && value2 == null)
            {
                return false;
            }

            if (value1 is string)
            {
                value1 = value1.ToString().Trim();
                value2 = value2.ToString().Trim();
                return Equals(value1, value2);
            }

            var selfValueComparer = value1 as IComparable;
            if (selfValueComparer != null && selfValueComparer.CompareTo(value2) != 0)
            {
                return false;
            }

            return Equals(value1, value2);
        }
    }
}
