using Desktop.Shared.Core;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos.OOE.Presets
{
    public class CompositePresetElementDto : BaseDto
    {
        [CSVAttribute("DTYPE", 2)]
        public virtual string Dtype { get; set; }

        [CSVAttribute("OWNING_PRESET_ID", 3)]
        public virtual Guid OwningPresetId { get; set; }

        [CSVAttribute("ASSOCIATED_PRESET_ID", 4)]
        public virtual Guid PresetForTypeElementId { get; set; }

        [CSVAttribute("TYPE_ELEMENT_ID", 5)]
        public virtual Guid CompositeTypeElementId { get; set; }

        [CSVAttribute("SUPER_TYPE_ID", 6)]
        public virtual Guid? SuperTypeId { get; set; }

        [CSVAttribute("OVERRIDE_DEFAULT_VALUE_LITERAL", 7)]
        public virtual string OverrideDefaultValueLiteral { get; set; }

        [CSVAttribute("INCLUDE_IN_INSTANTIATION", 8)]
        public virtual bool IncludeInInstantiation { get; set; }

        [Browsable(false)]
        [CSVAttribute("VERSION", 9)]
        public virtual int Version { get; set; }

        [Browsable(false)]
        public virtual State State { get; set; }
    }
}
