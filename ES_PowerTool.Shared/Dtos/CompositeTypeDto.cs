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
    public class CompositeTypeDto : BaseDto
    {
        [CSVAttribute("UNIQUE_NAME")]
        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_UNIQUE_NAME)]
        [Required]
        public virtual string UniqueName { get; set; }

        [CSVAttribute("DESCRIPTION")]
        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_DESCRIPTION)]
        [Required]
        public virtual string Description { get; set; }

        [CSVAttribute("RUNTIME_ID")]
        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_RUNTIME_ID)]
        [Required]
        public virtual Guid RuntimeId { get; set; }

        [CSVAttribute("BUILT_IN")]
        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_BUILT_IN)]
        public virtual bool BuiltIn { get; set; }

        [CSVAttribute("SORT_VALUE")]
        [Browsable(false)]
        public virtual int SortValue { get; set; }

        [CSVAttribute("DERIVABLE")]
        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_DERIVABLE)]
        public virtual bool Derivable { get; set; }

        [CSVAttribute("FOLDER_ID")]
        [Browsable(false)]
        public virtual Guid FolderId { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_SUPERTYPES)]
        [ListReference("SuperTypes")]
        [ReferenceEdiror("ES_PowerTool.Editors.TypeReferenceEditor", "ES_PowerTool")]
        public virtual ReferenceString SuperTypesReference { get; set; }
    }
}
