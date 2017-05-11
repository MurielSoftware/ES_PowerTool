using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    public class FolderDto : BaseDto
    {
        [CSVAttribute("NAME")]
        public virtual string Name { get; set; }

        [CSVAttribute("BUILT_IN")]
        public virtual bool BuiltIn { get; set; }

        [CSVAttribute("CONTENT_BUILT_IN")]
        public virtual bool ContentBuiltIn { get; set; }

        [CSVAttribute("SORT_VALUE")]
        public virtual int SortValue { get; set; }

        [CSVAttribute("PARENT_ID")]
        public virtual Guid FolderId { get; set; }
    }
}
