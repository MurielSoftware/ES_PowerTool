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
    [LocalizedDisplayName(MessageKeyConstants.LABEL_TYPE_ELEMENT)]
    public class CompositeTypeElementDto : BaseDto
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

        [CSVAttribute("OPTIONAL")]
        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_OPTIONAL)]
        public virtual bool Optional { get; set; }

        [CSVAttribute("RUNTIME_ID")]
        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_RUNTIME_ID)]
        [Required]
        public virtual Guid RuntimeId { get; set; }

        [CSVAttribute("SORT_VALUE")]
        [Browsable(false)]
        public virtual int SortValue { get; set; }

        [CSVAttribute("OWNING_TYPE_ID")]
        [Browsable(false)]
        public virtual Guid OwningTypeId { get; set; }

        [CSVAttribute("ELEMENT_TYPE_ID")]
        [Browsable(false)]
        public virtual Guid ElementTypeId { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_ELEMENT_TYPE)]
        [Reference("ElementType")]
        [ReferenceEdiror("ES_PowerTool.Editors.TypeReferenceEditor", "ES_PowerTool")]
        //[Editor("Editor.Ui.Editors.ModelReferenceEditor, Editor", "Editor.Core.Ui.Editors.BaseReferenceEditor, Editor.Core")]
        public virtual ReferenceString ModelReference { get; set; }
    }
}
