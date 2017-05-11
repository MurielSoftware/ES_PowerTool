using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    public class CompositeTypeDto : BaseDto
    {
        [CSVAttribute("UNIQUE_NAME")]
        public virtual string UniqueName { get; set; }

        [CSVAttribute("DESCRIPTION")]
        public virtual string Description { get; set; }

        [CSVAttribute("RUNTIME_ID")]
        public virtual Guid RuntimeId { get; set; }

        [CSVAttribute("BUILT_IN")]
        public virtual bool BuiltIn { get; set; }

        [CSVAttribute("SORT_VALUE")]
        public virtual int SortValue { get; set; }

        [CSVAttribute("DERIVABLE")]
        public virtual bool Derivable { get; set; }

        [CSVAttribute("FOLDER_ID")]
        public virtual Guid FolderId { get; set; }
    }
}
