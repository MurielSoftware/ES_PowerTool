using Desktop.Shared.Core.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Converters.References.Utils
{
    public class ReferenceConversionUtils
    {
        public static string GetReferencedId(ReferenceAttribute referenceAttribute)
        {
            return referenceAttribute.RefencedPropertyName + "Id";
        }

        public static bool IsCollectionPropertyType(Type type, ReferenceAttribute referenceAttribute)
        {
            PropertyInfo propertyInfo = type.GetProperty(referenceAttribute.RefencedPropertyName);
            return typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);
        }
    }
}
