using Desktop.Shared.Core;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using Desktop.Ui.I18n;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ES_PowerTool.Shared.Dtos
{
    [LocalizedDisplayName(MessageKeyConstants.LABEL_PRESET)]
    public class PresetDto : BaseDto
    {
        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY, 0)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_NAME)]
        [Required]
        [CSVAttribute("NAME", 2)]
        public virtual string Name { get; set; }

        [Browsable(false)]
        [CSVAttribute("TYPE_ID", 3)]
        public virtual Guid TypeId { get; set; }

        [Browsable(false)]
        [CSVAttribute("VERSION", 4)]
        public virtual int Version { get; set; }

        [Browsable(false)]
        [CSVAttribute("BUILT_IN", 5)]
        public virtual bool BuiltIn { get; set; }

        [Browsable(false)]
        public virtual State State { get; set; }

        [Browsable(false)]
        public virtual Guid ProjectId { get; set; }

        public PresetDto()
        {
            Version = 0;
        }
    }
}
