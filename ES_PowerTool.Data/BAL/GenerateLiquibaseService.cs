using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Data.DAL.OOE.Elements;
using Desktop.Shared.Core.Navigations;
using Desktop.Data.Core.Model;
using System.Text.RegularExpressions;
using ES_PowerTool.Data.DAL;
using Desktop.Data.Core.DAL;
using ES_PowerTool.Shared;

namespace ES_PowerTool.Data.BAL
{
    public class GenerateLiquibaseService : BaseService, IGenerateLiquibaseService
    {
        private CompositeTypeElementNavigationRepository _compositeTypeElementNavigationRepository;
        private GenericRepository _genericRepository;
        private SettingsRepository _settingsRepository;

        public GenerateLiquibaseService(Connection connection) 
            : base(connection)
        {
            _compositeTypeElementNavigationRepository = new CompositeTypeElementNavigationRepository(connection);
            _settingsRepository = new SettingsRepository(connection);
            _genericRepository = new GenericRepository(connection);
        }

        public List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem> GetCompositeTypeElementsToGenerate(Guid projectId)
        {
            List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem> generateLiquibaseCompositeTypeElementTreeNavigationItems = new List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem>();
            List<CompositeTypeElementTreeNavigationItem> compositeTypeElementTreeNavigationItems = _compositeTypeElementNavigationRepository.FindNewCompositeTypeElementsWhereElementTypeIsPrimitive(projectId);
            foreach(CompositeTypeElementTreeNavigationItem compositeTypeElementTreeNavigationItem in compositeTypeElementTreeNavigationItems)
            {
                generateLiquibaseCompositeTypeElementTreeNavigationItems.Add(CreateGenerateLiquibaseCompositeTypeElementTreeNavigationItem(compositeTypeElementTreeNavigationItem));
            }
            return generateLiquibaseCompositeTypeElementTreeNavigationItems;
        }

        public string GenerateCompositeTypeElements(List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem> compositeTypeElementTreeNavigationItemsToGenerate)
        {
            compositeTypeElementTreeNavigationItemsToGenerate = compositeTypeElementTreeNavigationItemsToGenerate.Where(x => x.Generate).ToList();
            Dictionary<string, Settings> liquibaseDataTypeConversionToName = GetLiquibaseDataTypeConversionToName();
            Settings liquibaseFormatColumn = _genericRepository.Find<Settings>(IdConstants.SETTINGS_LIQUIBASE_COLUMN_FORMAT_ID);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (GenerateLiquibaseCompositeTypeElementTreeNavigationItem compositeTypeElementTreeNavigationItemToGenerate in compositeTypeElementTreeNavigationItemsToGenerate)
            {
                string liquibaseDataType = liquibaseDataTypeConversionToName[compositeTypeElementTreeNavigationItemToGenerate.CompositeTypeElementTreeNavigationItem.ElementTypeName].Value;
                string column = string.Format(liquibaseFormatColumn.Value, compositeTypeElementTreeNavigationItemToGenerate.ColumnName, liquibaseDataType);
                stringBuilder.AppendLine(column);
            }
            return stringBuilder.ToString();
        }

        private GenerateLiquibaseCompositeTypeElementTreeNavigationItem CreateGenerateLiquibaseCompositeTypeElementTreeNavigationItem(CompositeTypeElementTreeNavigationItem compositeTypeElementTreeNavigationItem)
        {
            GenerateLiquibaseCompositeTypeElementTreeNavigationItem generateLiquibaseCompositeTypeElementTreeNavigationItem = new GenerateLiquibaseCompositeTypeElementTreeNavigationItem();
            generateLiquibaseCompositeTypeElementTreeNavigationItem.CompositeTypeElementTreeNavigationItem = compositeTypeElementTreeNavigationItem;
            generateLiquibaseCompositeTypeElementTreeNavigationItem.ColumnName = CreateColumnName(compositeTypeElementTreeNavigationItem.Name);
            generateLiquibaseCompositeTypeElementTreeNavigationItem.Generate = true;
            return generateLiquibaseCompositeTypeElementTreeNavigationItem;
        }

        private string CreateColumnName(string name)
        {
            string output = Regex.Replace(name, "([a-z?])([A-Z])", "$1_$2");
            return output.ToUpper();
        }

        private Dictionary<string, Settings> GetLiquibaseDataTypeConversionToName()
        {
            return _settingsRepository.FindLiquibaseDataTypeConversion().ToDictionary(x => x.Name, x => x);
        }
    }
}
