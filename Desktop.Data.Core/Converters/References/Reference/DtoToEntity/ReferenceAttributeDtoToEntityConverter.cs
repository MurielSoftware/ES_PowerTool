using Desktop.Data.Core.Converters.References.Utils;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Desktop.Data.Core.Converters.References.Reference.DtoToEntity
{
    /// <summary>
    /// Base DTO to entity reference converter.
    /// </summary>
    /// <typeparam name="T">The type of the DTO</typeparam>
    /// <typeparam name="U">The type if the Entity</typeparam>
    public class ReferenceAttributeDtoToEntityConverter<T, U> : BaseReferenceAttributeConverter<T, U>
        where T : BaseDto
        where U : BaseEntity
    {
        private List<IReferenceConverter> _referenceConverters = new List<IReferenceConverter>();

        public override ICollection<PropertyInfo> GetPropertiesToConvert(T source, U target)
        {
            return source.GetType().GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(ReferenceAttribute)) && !Attribute.IsDefined(x, typeof(ListReferenceAttribute)))
                .ToList();
        }

        public override void Convert(Connection connection, T dto, U entity, PropertyInfo sourcePropertyInfo)
        {
            ReferenceAttribute referenceAttribute = sourcePropertyInfo.GetCustomAttribute<ReferenceAttribute>();
            ReferenceString referenceString = sourcePropertyInfo.GetValue(dto) as ReferenceString;
            Type referencedEntityType = GetReferencedEntityType(entity, referenceAttribute);
            IReferenceConverter referenceConverter = GetReferenceConverter(referenceAttribute, typeof(U), referencedEntityType);
            referenceConverter.Convert(connection, entity, dto, sourcePropertyInfo, referenceAttribute, referenceString);
        }

        private IReferenceConverter GetReferenceConverter(ReferenceAttribute referenceAttribute, Type sourceEntityType, Type referencedEntityType)
        {
            //if (ReferenceConversionUtils.IsCollectionPropertyType(sourceEntityType, referenceAttribute))
            //{
            //    return CreateReferenceConverter(typeof(MultiReferenceAttributeDtoToEntityConverter<,>), sourceEntityType, referencedEntityType);
            //}
            return CreateReferenceConverter(typeof(SingleReferenceAttributeDtoToEntityConverter<,>), sourceEntityType, referencedEntityType);
        }
    }
}