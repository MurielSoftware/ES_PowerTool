using Desktop.Shared.Core.Attributes;
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
        [CSVAttribute("UNIQUE_NAME")]
        public virtual string UniqueName { get; set; }

        [CSVAttribute("DESCRIPTION")]
        public virtual string Description { get; set; }

        [CSVAttribute("OPTIONAL")]
        public virtual bool Optional { get; set; }

        [CSVAttribute("RUNTIME_ID")]
        public virtual Guid RuntimeId { get; set; }

        [CSVAttribute("SORT_VALUE")]
        public virtual int SortValue { get; set; }

        [CSVAttribute("OWNING_TYPE_ID")]
        public virtual Guid OwningTypeId { get; set; }

        [CSVAttribute("ELEMENT_TYPE_ID")]
        public virtual Guid ElementTypeId { get; set; }
    }
}
