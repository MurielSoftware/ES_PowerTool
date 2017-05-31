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
        public virtual SettingValueDto LiquibaseAddColumnFormat { get; set; }
        public virtual List<SettingValueDto> SettingsLiquibaseDataTypeConversion { get; set; }
        public virtual List<SettingValueDto> SettingsCodeDataTypeConversion { get; set; }

        public SettingsDto()
        {
        }

        public List<SettingValueDto> GetAllSettingValues()
        {
            List<SettingValueDto> settingValueDtos = new List<SettingValueDto>();
            settingValueDtos.AddRange(SettingsLiquibaseDataTypeConversion);
            settingValueDtos.AddRange(SettingsCodeDataTypeConversion);
            return settingValueDtos;
        }

        //public List<SettingValueDto> GetAllChangedValues()
        //{
        //    List<SettingValueDto> settingValueDtos = new List<SettingValueDto>();
        //    settingValueDtos.AddRange(SettingsLiquibaseDataTypeConversion.Where(x => x.Changed));
        //    return settingValueDtos;
        //}
    }
}
