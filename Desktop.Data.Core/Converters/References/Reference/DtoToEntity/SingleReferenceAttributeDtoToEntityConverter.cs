using Desktop.Data.Core.Converters.References.Utils;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using System.Reflection;

namespace Desktop.Data.Core.Converters.References.Reference.DtoToEntity
{
    /// <summary>
    /// Converts the single reference from DTO to Entity
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the referenced entity</typeparam>
    public class SingleReferenceAttributeDtoToEntityConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(Connection connection, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo targetProperty = sourceEntity.GetType().GetProperty(ReferenceConversionUtils.GetReferencedId(referenceAttribute));
            if (referenceString == null || string.IsNullOrEmpty(referenceString.GetValue()))
            {
                targetProperty.SetValue(sourceEntity, null);
            }
            else
            {
                targetProperty.SetValue(sourceEntity, referenceString.GetId());
            }
        }
    }
}
