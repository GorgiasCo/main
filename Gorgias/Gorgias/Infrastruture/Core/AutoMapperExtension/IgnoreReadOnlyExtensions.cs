using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Gorgias.Infrastruture.Core.AutoMapperExtension
{
    public static class IgnoreReadOnlyExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreReadOnly<TSource, TDestination>(
                   this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);

            foreach (var property in sourceType.GetProperties())
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                ReadOnlyAttribute attribute = (ReadOnlyAttribute)descriptor.Attributes[typeof(ReadOnlyAttribute)];
                if (attribute.IsReadOnly == true)
                    expression.ForMember(property.Name, opt => opt.Ignore());
            }
            return expression;
        }
    }
}