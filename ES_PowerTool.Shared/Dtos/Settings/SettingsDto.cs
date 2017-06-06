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
        public virtual SettingValueDto AllowEditImportedElements { get; set; }
        public virtual SettingValueDto LiquibaseAddColumnFormat { get; set; }
        public virtual List<SettingValueDto> SettingsLiquibaseDataTypeConversion { get; set; }
        public virtual List<SettingValueDto> SettingsCodeDataTypeConversion { get; set; }

        public SettingsDto()
        {
        }

        public void SettAllSetingValues(List<SettingValueDto> settingValueDtos)
        {
            AllowEditImportedElements = settingValueDtos.Where(x => x.Id == IdConstants.SETTINGS_COMMON_EDIT_IMPORTED_ELEMENTS_ID).SingleOrDefault();
            LiquibaseAddColumnFormat = settingValueDtos.Where(x => x.Id == IdConstants.SETTINGS_LIQUIBASE_COLUMN_FORMAT_ID).SingleOrDefault();
            SettingsLiquibaseDataTypeConversion = settingValueDtos.Where(x => x.Group == SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE).ToList();
            SettingsCodeDataTypeConversion = settingValueDtos.Where(x => x.Group == SettingsGroup.CODE_CONVERT_DATA_TYPE).ToList();
        }

        public List<SettingValueDto> GetAllSettingValues()
        {
            List<SettingValueDto> settingValueDtos = new List<SettingValueDto>();
            settingValueDtos.AddRange(SettingsLiquibaseDataTypeConversion);
            settingValueDtos.AddRange(SettingsCodeDataTypeConversion);
            settingValueDtos.Add(LiquibaseAddColumnFormat);
            settingValueDtos.Add(AllowEditImportedElements);
            return settingValueDtos;
        }
    }
}
