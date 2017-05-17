using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    [LocalizedDisplayName(MessageKeyConstants.LABEL_TYPE)]
    public class CompositeTypeDto : ModelObjectTypeDto
    {
        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_SUPERTYPES)]
        [ListReference("SuperTypes")]
        [ReferenceEdiror("ES_PowerTool.Editors.TypeReferenceDerivableEditor", "ES_PowerTool")]
        public virtual ReferenceString SuperTypesReference { get; set; }
    }
}
