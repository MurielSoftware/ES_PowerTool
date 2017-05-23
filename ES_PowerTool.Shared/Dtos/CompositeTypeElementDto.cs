using Desktop.Shared.Core;
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
        [Browsable(false)]
        [CSVAttribute("SORT_VALUE", 2)]
        public virtual int SortValue { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY, 0)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_DESCRIPTION)]
        [Required]
        [CSVAttribute("DESCRIPTION", 3)]
        public virtual string Description { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY, 0)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_UNIQUE_NAME)]
        [Required]
        [CSVAttribute("UNIQUE_NAME", 4)]
        public virtual string UniqueName { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY, 1)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_RUNTIME_ID)]
        [Required]
        [CSVAttribute("RUNTIME_ID", 5)]
        public virtual Guid RuntimeId { get; set; }

        [Browsable(false)]
        [CSVAttribute("OWNING_TYPE_ID", 6)]
        public virtual Guid OwningTypeId { get; set; }

        [Browsable(false)]
        [CSVAttribute("ELEMENT_TYPE_ID", 7)]
        public virtual Guid ElementTypeId { get; set; }

        [Browsable(false)]
        [CSVAttribute("DEFAULT_VALUE_LITERAL", 8)]
        public virtual string DefaultValueLiteral { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY, 1)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_OPTIONAL)]
        [CSVAttribute("OPTIONAL", 9)]
        public virtual bool Optional { get; set; }

        [Browsable(false)]
        [CSVAttribute("VERSION", 10)]
        public virtual int Version { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY, 1)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_ELEMENT_TYPE)]
        [Reference("ElementType")]
        [ReferenceEdiror("ES_PowerTool.Editors.TypeReferenceEditor", "ES_PowerTool")]
        public virtual ReferenceString ModelReference { get; set; }

        [Browsable(false)]
        public virtual State State { get; set; }

        [Browsable(false)]
        public virtual Guid ProjectId { get; set; }

        public CompositeTypeElementDto()
        {
            DefaultValueLiteral = null;
            Version = 0;
        }
    }
}
