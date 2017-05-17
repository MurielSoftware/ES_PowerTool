using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class CompositeType : BaseEntity
    {
        [Required]
        public virtual string UniqueName { get; set; }

        [Required]
        public virtual string Description { get; set; }

        public virtual Guid RuntimeId { get; set; }
        public virtual bool BuiltIn { get; set; }
        public virtual int SortValue { get; set; }
        public virtual bool? Derivable { get; set; }

        public virtual Guid FolderId { get; set; }

        [ForeignKey("FolderId")]
        public virtual Folder Folder { get; set; }

        [InverseProperty("OwningType")]
        public virtual ICollection<CompositeTypeElement> Children { get; set; }

        public virtual ICollection<CompositeType> SuperTypes { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
