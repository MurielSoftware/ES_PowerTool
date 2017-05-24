using Desktop.Shared.Core.Attributes;
using System;

namespace ES_PowerTool.Shared.Dtos.Generate
{
    public class DefaultPresetGenearateDto
    {
        [CSVAttribute("ID", 0)]
        public Guid Id { get; set; }

        [CSVAttribute("DEFAULT_PRESET_ID", 1)]
        public Guid DefaultPresetId { get; set; }
    }
}
