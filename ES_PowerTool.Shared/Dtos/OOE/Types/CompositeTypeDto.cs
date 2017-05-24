using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.DataTypes;
using Desktop.Ui.I18n;

namespace ES_PowerTool.Shared.Dtos.OOE.Types
{
    [LocalizedDisplayName(MessageKeyConstants.LABEL_TYPE)]
    public class CompositeTypeDto : ModelObjectTypeDto
    {
        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY, 1)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_SUPERTYPES)]
        [ListReference("SuperTypes")]
        [ReferenceEdiror("ES_PowerTool.Editors.TypeReferenceDerivableEditor", "ES_PowerTool")]
        public virtual ReferenceString SuperTypesReference { get; set; }
    }
}
