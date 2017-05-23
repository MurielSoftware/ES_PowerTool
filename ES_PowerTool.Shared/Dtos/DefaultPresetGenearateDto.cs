using Desktop.Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    public class DefaultPresetGenearateDto
    {
        [CSVAttribute("ID", 0)]
        public Guid Id { get; set; }

        [CSVAttribute("DEFAULT_PRESET_ID", 1)]
        public Guid DefaultPresetId { get; set; }
    }
}
