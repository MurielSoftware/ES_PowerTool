using Desktop.Shared.Core;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos.OOE.Presets
{
    [LocalizedDisplayName(MessageKeyConstants.LABEL_COMPOSITE_PRESET_ELEMENT)]
    public class CompositePresetElementDto : BaseDto
    {
        [CSVAttribute("DTYPE", 2)]
        [Browsable(false)]
        public virtual string Dtype { get; set; }

        [CSVAttribute("OWNING_PRESET_ID", 3)]
        [Browsable(false)]
        public virtual Guid OwningPresetId { get; set; }

        [CSVAttribute("ASSOCIATED_PRESET_ID", 4)]
        [Browsable(false)]
        public virtual Guid PresetForTypeElementId { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY, 1)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_PRESET)]
        [Reference("PresetForTypeElement")]
        [ReferenceEdiror("ES_PowerTool.Editors.PresetReferenceEditor", "ES_PowerTool")]
        public virtual ReferenceString PresetForTypeElementReference { get; set; }

        [CSVAttribute("TYPE_ELEMENT_ID", 5)]
        [Browsable(false)]
        public virtual Guid CompositeTypeElementId { get; set; }

        [CSVAttribute("SUPER_TYPE_ID", 6)]
        [Browsable(false)]
        public virtual Guid? SuperTypeId { get; set; }

        [CSVAttribute("OVERRIDE_DEFAULT_VALUE_LITERAL", 7)]
        [Browsable(false)]
        public virtual string OverrideDefaultValueLiteral { get; set; }

        [CSVAttribute("INCLUDE_IN_INSTANTIATION", 8)]
        [Browsable(false)]
        public virtual bool IncludeInInstantiation { get; set; }

        [Browsable(false)]
        [CSVAttribute("VERSION", 9)]
        public virtual int Version { get; set; }

        [Browsable(false)]
        public virtual State State { get; set; }
    }
}
