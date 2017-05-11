using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using ES_PowerTool.Data.Converters.References.Utils;
using ES_PowerTool.Data.Model;
using System.Reflection;

namespace ES_PowerTool.Data.Converters.References.Reference.DtoToEntity
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
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
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
