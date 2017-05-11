using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    public class CompositeTypeElementDto : BaseDto
    {
        public virtual string UniqueName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Optional { get; set; }
        public virtual Guid RuntimeId { get; set; }

        public virtual Guid OwningTypeId { get; set; }
        public virtual Guid ElementTypeId { get; set; }
    }
}
