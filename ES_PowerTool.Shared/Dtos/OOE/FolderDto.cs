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

namespace ES_PowerTool.Shared.Dtos.OOE
{
    [LocalizedDisplayName(MessageKeyConstants.LABEL_FOLDER)]
    public class FolderDto : BaseDto
    {
        [Browsable(false)]
        [CSVAttribute("SORT_VALUE", 2)]
        public virtual int SortValue { get; set; }

        [Browsable(false)]
        [CSVAttribute("PARENT_ID", 3)]
        public virtual Guid? ParentId { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY, 0)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_NAME)]
        [Required]
        [CSVAttribute("NAME", 4)]
        public virtual string Name { get; set; }

        [Browsable(false)]
        [CSVAttribute("TYPE", 5)]
        public virtual int Type { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY, 1)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_BUILT_IN)]
        [CSVAttribute("BUILT_IN", 6)]
        public virtual bool BuiltIn { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY, 1)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_CONTENT_BUILT_IN)]
        [CSVAttribute("CONTENT_BUILT_IN", 7)]
        public virtual bool ContentBuiltIn { get; set; }

        [Browsable(false)]
        [CSVAttribute("VERSION", 8)]
        public virtual int Version { get; set; }

        [Browsable(false)]
        public virtual Guid ProjectId { get; set; }

        [Browsable(false)]
        public virtual State State { get; set; }

        public FolderDto()
        {
            Type = 1;
            Version = 0;
        }
    }
}
