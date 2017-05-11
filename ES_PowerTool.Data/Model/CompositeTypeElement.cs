using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Model
{
    public class CompositeTypeElement : BaseEntity
    {
        public virtual string UniqueName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Optional { get; set; }
        public virtual Guid RuntimeId { get; set; }
        public virtual int SortValue { get; set; }

        public virtual Guid OwningTypeId { get; set; }
        public virtual Guid ElementTypeId { get; set; }

        [ForeignKey("OwningTypeId")]
        public virtual CompositeType OwningType { get; set; }

        [ForeignKey("ElementTypeId")]
        public virtual CompositeType ElementType { get; set; }
    }
}
