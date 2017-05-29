using Desktop.Shared.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class Preset : BaseEntity, IProjectAwareEntity, IStateAwareEntity
    {
        public virtual string Name { get; set; }      
        public virtual Guid TypeId { get; set; }
        public virtual bool BuiltIn { get; set; }
        public virtual Guid ProjectId { get; set; }
        public virtual State State { get; set; }

        [ForeignKey("TypeId")]
        public virtual CompositeType Type { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [InverseProperty("OwningPreset")]
        public virtual ICollection<CompositePresetElement> Elements { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
