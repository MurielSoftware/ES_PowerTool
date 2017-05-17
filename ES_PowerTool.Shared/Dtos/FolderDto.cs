using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    [LocalizedDisplayName(MessageKeyConstants.LABEL_FOLDER)]
    public class FolderDto : BaseDto
    {
        [CSVAttribute("NAME")]
        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_NAME)]
        [Required]
        public virtual string Name { get; set; }

        [CSVAttribute("BUILT_IN")]
        [Browsable(false)]
        public virtual bool BuiltIn { get; set; }

        [CSVAttribute("CONTENT_BUILT_IN")]
        [Browsable(false)]
        public virtual bool ContentBuiltIn { get; set; }

        [CSVAttribute("SORT_VALUE")]
        [Browsable(false)]
        public virtual int SortValue { get; set; }

        [CSVAttribute("PARENT_ID")]
        [Browsable(false)]
        public virtual Guid? ParentId { get; set; }

        [Browsable(false)]
        public virtual Guid ProjectId { get; set; }
    }
}
