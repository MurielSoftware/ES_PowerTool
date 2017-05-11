using Desktop.Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ES_PowerTool.Data.Converters.References.List
{
    /// <summary>
    /// Converts the list single reference
    /// </summary>
    /// <typeparam name="T">The type of the source</typeparam>
    /// <typeparam name="U">The type of the target</typeparam>
    public abstract class BaseListReferenceAttributeConverter<T, U> : BaseReferenceAttributeConverter<T, U>, IConverter<T, U>
    {
        public override ICollection<PropertyInfo> GetPropertiesToConvert(T source, U target)
        {
            return target.GetType().GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(ListReferenceAttribute)))
                .ToList();
        }
    }
}
