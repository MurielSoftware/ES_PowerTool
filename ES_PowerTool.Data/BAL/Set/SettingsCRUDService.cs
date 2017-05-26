using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos.Settings;
using ES_PowerTool.Shared.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Services;
using Desktop.Data.Core.Converters;
using Desktop.Data.Core.DAL;
using ES_PowerTool.Shared;

namespace ES_PowerTool.Data.BAL.Set
{
    public class SettingsCRUDService : BaseService, ISettingsCRUDService
    {
        protected GenericRepository _genericRepository;
        protected BaseConvertProvider<SettingValueDto, Settings> _dtoToEntityConverter = new DtoToEntityConvertProvider<SettingValueDto, Settings>();
        protected BaseConvertProvider<Settings, SettingValueDto> _entityToDtoConverter = new EntityToDtoConvertProvider<Settings, SettingValueDto>();

        private const int SETTINGS_COUNT = 11;

        public SettingsCRUDService(Connection connection) 
            : base(connection)
        {
            _genericRepository = new GenericRepository(connection);
        }

        public SettingsDto Persist(SettingsDto settingsDto)
        {
            List<Settings> settings = new List<Settings>();
            List<SettingValueDto> settingValueDtos = settingsDto.GetAllSettingValues();
            settingValueDtos.ForEach(x => settings.Add(_dtoToEntityConverter.Convert(_connection, x)));
            settings.ForEach(x => _genericRepository.Persist<Settings>(x));
            return settingsDto;
        }

        public SettingsDto Read(Guid id)
        {
            SettingsDto settingsDto = new SettingsDto();
            List<SettingValueDto> settingValueDtos = new List<SettingValueDto>();
            List<Settings> settings = _genericRepository.FindAll<Settings>();
            if(settings.Count != SETTINGS_COUNT)
            {
                settings = CreateAndPresistDefaultSettings();
            }
            settings.ForEach(x => settingValueDtos.Add(_entityToDtoConverter.Convert(_connection, x)));
            settingsDto.LiquibaseAddColumnFormat = settingValueDtos.Where(x => x.Id == IdConstants.SETTINGS_LIQUIBASE_COLUMN_FORMAT_ID).SingleOrDefault();
            settingsDto.SettingsLiquibaseDataTypeConversion = settingValueDtos.Where(x => x.Group == SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE).ToList();
            return settingsDto;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        private List<Settings> CreateAndPresistDefaultSettings()
        {
            _genericRepository.DeleteRange<Settings>(x => x.Id != Guid.Empty);

            List<Settings> settings = new List<Settings>();
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_COLUMN_FORMAT_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_COMMON, "Column definition", "<column name=\"{0}\" type=\"{1}\" />"));
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_BOOLEAN_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "boolean", "BOOLEAN"));
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_BYTE_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "byte", "NUMBER"));
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_DATE_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "Date", "date"));
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_DOUBLE_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "double", "NUMBER"));
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_FLOAT_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "float", "NUMBER"));
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_INT_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "int", "NUMBER"));                                   
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_LONG_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "long", "NUMBER"));
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_SHORT_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "short", "NUMBER"));
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_STRING_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "java.lang.String", "VARCHAR2(255)"));
            settings.Add(CreateSettings(IdConstants.SETTINGS_LIQUIBASE_DATA_TYPE_CONVERSION_UUID_ID, SettingsSection.LIQUIBASE, SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE, "java.util.UUID", "UUID"));
            _genericRepository.PersistAsNews<Settings>(settings);
            return settings;
        }

        private Settings CreateSettings(Guid id, SettingsSection settingsSection, SettingsGroup settingsGroup, string name, string value)
        {
            Settings settings = new Settings();
            settings.Id = id;
            settings.Section = settingsSection;
            settings.Group = settingsGroup;
            settings.Name = name;
            settings.Value = value;
            return settings;
        }
    }
}
