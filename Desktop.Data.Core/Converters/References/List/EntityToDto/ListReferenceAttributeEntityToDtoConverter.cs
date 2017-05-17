using Desktop.Data.Core.Converters.References.List.EntityToDto;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Desktop.Data.Core.Converters.References.List
{
    /// <summary>
    /// Converts the list single reference
    /// </summary>
    /// <typeparam name="T">The type of the source</typeparam>
    /// <typeparam name="U">The type of the target</typeparam>
    public class ListReferenceAttributeEntityToDtoConverter<T, U> : BaseReferenceAttributeConverter<T, U>//, IConverter<T, U>
        where T : BaseEntity
        where U : BaseDto
    {
        public override ICollection<PropertyInfo> GetPropertiesToConvert(T source, U target)
        {
            return target.GetType().GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(ListReferenceAttribute)))
                .ToList();
        }

        public override void Convert(Connection connection, T entity, U dto, PropertyInfo sourcePropertyInfo)
        {
            ReferenceAttribute referenceAttribute = sourcePropertyInfo.GetCustomAttribute<ReferenceAttribute>();
            ReferenceString referenceString = sourcePropertyInfo.GetValue(dto) as ReferenceString;
            Type referencedEntityType = GetReferencedEntityType(entity, referenceAttribute);
            IReferenceConverter referenceConverter = GetReferenceConverter(referenceAttribute, typeof(T), referencedEntityType);
            referenceConverter.Convert(connection, entity, dto, sourcePropertyInfo, referenceAttribute, referenceString);
        }

        private IReferenceConverter GetReferenceConverter(ReferenceAttribute referenceAttribute, Type sourceEntityType, Type referencedEntityType)
        {
            //if (ReferenceConversionUtils.IsCollectionPropertyType(sourceEntityType, referenceAttribute))
            //{
            //    return CreateReferenceConverter(typeof(MultiReferenceAttributeEntityToDtoConverter<,>), sourceEntityType, referencedEntityType);
            //}
            return CreateReferenceConverter(typeof(MultiListReferenceAttributeEntityToDtoConverter<,>), sourceEntityType, referencedEntityType);
        }
    }
}
