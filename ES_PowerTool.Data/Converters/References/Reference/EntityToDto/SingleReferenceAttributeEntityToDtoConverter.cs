using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using ES_PowerTool.Data.Converters.References.Utils;
using ES_PowerTool.Data.Model;
using System.Reflection;

namespace ES_PowerTool.Data.Converters.References.Reference.EntityToDto
{
    /// <summary>
    /// Converts the single reference from Entity to DTO.
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the target entity</typeparam>
    public class SingleReferenceAttributeEntityToDtoConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo referencedEntityPropertyInfo = sourceEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            PropertyInfo referencedEntityIdPropertyInfo = sourceEntity.GetType().GetProperty(ReferenceConversionUtils.GetReferencedId(referenceAttribute));
            U referencedEntity = (U)referencedEntityPropertyInfo.GetValue(sourceEntity, null);
            if(referencedEntity != null)
            {
                sourcePropertyInfo.SetValue(dto, new ReferenceString(referencedEntity.Id, referencedEntity.ToString()));
            }
        }
    }
}
