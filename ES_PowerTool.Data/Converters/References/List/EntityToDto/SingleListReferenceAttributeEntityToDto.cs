using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using ES_PowerTool.Data.Converters.References;
using ES_PowerTool.Data.Converters.References.Utils;
using ES_PowerTool.Data.Model;
using System.Reflection;

namespace Server.Converters.References.List.EntityToDto
{
    /// <summary>
    /// Converts the single list reference from Entity to DTO.
    /// </summary>
    /// <typeparam name="T">The type of the source Entity</typeparam>
    /// <typeparam name="U">The type of the referenced Entity</typeparam>
    public class SingleListReferenceAttributeEntityToDto<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto targetDto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo referencedEntityPropertyInfo = sourceEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            PropertyInfo referencedEntityIdPropertyInfo = sourceEntity.GetType().GetProperty(ReferenceConversionUtils.GetReferencedId(referenceAttribute));
            U referencedEntity = (U)referencedEntityPropertyInfo.GetValue(sourceEntity, null);
            if (referencedEntity != null)
            {
                sourcePropertyInfo.SetValue(targetDto, new ReferenceString(referencedEntity.Id, referencedEntity.ToString()));
            }
        }
    }
}
