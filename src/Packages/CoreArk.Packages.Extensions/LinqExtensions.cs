using System;
using System.Collections.Generic;

namespace CoreArk.Packages.Extensions
{
    public static class LinqExtensions
    {
        public static (object Value, Type Type) GetValue<TClass>(this TClass tclass, string valuePath)
            where TClass : class
        {
            if (valuePath.Contains("."))
            {
                var splittedValuePath = valuePath.Split(".", StringSplitOptions.RemoveEmptyEntries);
                var property = tclass.GetType().GetProperty(splittedValuePath[0]);
                _ = property ?? throw new Exception(
                    $"Object of type {typeof(TClass)} does not have a property with name {splittedValuePath[0]}");
                return GetValue(property.GetValue(tclass, null), valuePath.Substring(valuePath.IndexOf(".") + 1));
            }

            var baseProperty = tclass.GetType().GetProperty(valuePath);
            _ = baseProperty ??
                throw new Exception($"Object of type {typeof(TClass)} does not have a property with name {valuePath}");
            return (baseProperty.GetValue(tclass), baseProperty.GetType());
        }
    }
}