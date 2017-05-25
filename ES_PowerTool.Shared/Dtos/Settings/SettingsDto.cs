using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos.Settings
{
    public class SettingsDto : BaseDto
    {
        public virtual List<SettingValueDto> SettingsLiquibaseDatabase { get; set; }

        public SettingsDto()
        {
            SettingsLiquibaseDatabase = new List<SettingValueDto>();
            SettingsLiquibaseDatabase.Add(new SettingValueDto() { Name = "int", Value = "NUMBER" });
        }
    }
}
