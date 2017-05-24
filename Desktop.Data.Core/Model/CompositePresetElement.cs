using Desktop.Shared.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class CompositePresetElement : BaseEntity
    {
        public virtual string Dtype { get; set; }
        public virtual Guid OwningPresetId { get; set; }
        public virtual Guid PresetForTypeElementId { get; set; }
        public virtual Guid CompositeTypeElementId { get; set; }
        public virtual Guid? SuperTypeId { get; set; }
        public virtual string OverrideDefaultValueLiteral { get; set; }
        public virtual bool IncludeInInstantiation { get; set; }

        public virtual State State { get; set; }

        [ForeignKey("OwningPresetId")]
        public virtual Preset OwningPreset { get; set; }

        [ForeignKey("CompositeTypeElementId")]
        public virtual CompositeTypeElement CompositeTypeElement { get; set; }

        [ForeignKey("PresetForTypeElementId")]
        public virtual Preset PresetForTypeElement { get; set; }
    }
}
