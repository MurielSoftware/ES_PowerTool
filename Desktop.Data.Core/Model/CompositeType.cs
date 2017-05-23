using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class CompositeType : ModelObjectType
    {
        public static string DISC = "COM";

        public virtual Guid? DefaultPresetId { get; set; }
        public virtual Guid? DefaultWidgetTypeId { get; set; }

        [ForeignKey("DefaultPresetId")]
        public virtual Preset DefaultPreset { get; set; }

        public virtual ICollection<CompositeType> SuperTypes { get; set; }

        [InverseProperty("OwningType")]
        public virtual ICollection<CompositeTypeElement> Children { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<Preset> Presets { get; set; }
    }
}
