using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class Project : BaseEntity
    {
        [Required]
        public virtual string Name { get; set; }

        public virtual ICollection<Folder> Folders { get; set; }
        public virtual ICollection<ModelObjectType> ModelObjectTypes { get; set; }
        public virtual ICollection<CompositeTypeElement> CompositeTypeElements { get; set; }
        public virtual ICollection<Preset> Presets { get; set; }
    }
}
