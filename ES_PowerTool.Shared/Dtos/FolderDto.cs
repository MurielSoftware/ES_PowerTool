using Desktop.Shared.Core;
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
        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_NAME)]
        [Required]
        [CSVAttribute("NAME")]
        public virtual string Name { get; set; }

        [Browsable(false)]
        [CSVAttribute("BUILT_IN")]
        public virtual bool BuiltIn { get; set; }

        [Browsable(false)]
        [CSVAttribute("CONTENT_BUILT_IN")]
        public virtual bool ContentBuiltIn { get; set; }

        [Browsable(false)]
        [CSVAttribute("SORT_VALUE")]
        public virtual int SortValue { get; set; }

        [Browsable(false)]

        [CSVAttribute("PARENT_ID")]
        public virtual Guid? ParentId { get; set; }

        [Browsable(false)]
        public virtual Guid ProjectId { get; set; }

        [Browsable(false)]
        public virtual State State { get; set; }
    }
}
