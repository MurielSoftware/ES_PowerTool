using Desktop.Shared.Core;
using Desktop.Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class CompositeTypeElement : BaseEntity, IProjectAwareEntity, IStateAwareEntity
    {
        public virtual int SortValue { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        public virtual string UniqueName { get; set; }

        public virtual Guid RuntimeId { get; set; }
        public virtual Guid OwningTypeId { get; set; }
        public virtual Guid ElementTypeId { get; set; }
        public virtual string DefaultValueLiteral { get; set; }
        public virtual bool Optional { get; set; }

        public virtual Guid ProjectId { get; set; }
        public virtual State State { get; set; }
        
        [ForeignKey("OwningTypeId")]
        public virtual CompositeType OwningType { get; set; }

        [ForeignKey("ElementTypeId")]
        public virtual ModelObjectType ElementType { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
