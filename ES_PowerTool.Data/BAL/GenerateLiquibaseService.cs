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

namespace ES_PowerTool.Data.BAL
{
    public class GenerateLiquibaseService : BaseService, IGenerateLiquibaseService
    {
        private CompositeTypeElementNavigationRepository _compositeTypeElementNavigationRepository;

        public GenerateLiquibaseService(Connection connection) 
            : base(connection)
        {
            _compositeTypeElementNavigationRepository = new CompositeTypeElementNavigationRepository(connection);
        }

        public List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem> GetCompositeTypeElementsToGenerate(Guid projectId)
        {
            List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem> generateLiquibaseCompositeTypeElementTreeNavigationItems = new List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem>();
            List<CompositeTypeElementTreeNavigationItem> compositeTypeElementTreeNavigationItems = _compositeTypeElementNavigationRepository.FindCompositeTypeElementsToExport(projectId);
            foreach(CompositeTypeElementTreeNavigationItem compositeTypeElementTreeNavigationItem in compositeTypeElementTreeNavigationItems)
            {
                generateLiquibaseCompositeTypeElementTreeNavigationItems.Add(CreateGenerateLiquibaseCompositeTypeElementTreeNavigationItem(compositeTypeElementTreeNavigationItem));
            }
            return generateLiquibaseCompositeTypeElementTreeNavigationItems;
        }

        public string GenerateCompositeTypeElements(List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem> compositeTypeElementTreeNavigationItemsToGenerate)
        {
            compositeTypeElementTreeNavigationItemsToGenerate = compositeTypeElementTreeNavigationItemsToGenerate.Where(x => x.Generate).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (GenerateLiquibaseCompositeTypeElementTreeNavigationItem compositeTypeElementTreeNavigationItemToGenerate in compositeTypeElementTreeNavigationItemsToGenerate)
            {
                stringBuilder.AppendLine(compositeTypeElementTreeNavigationItemToGenerate.ToString());
            }
            return stringBuilder.ToString();
        }

        private GenerateLiquibaseCompositeTypeElementTreeNavigationItem CreateGenerateLiquibaseCompositeTypeElementTreeNavigationItem(CompositeTypeElementTreeNavigationItem compositeTypeElementTreeNavigationItem)
        {
            GenerateLiquibaseCompositeTypeElementTreeNavigationItem generateLiquibaseCompositeTypeElementTreeNavigationItem = new GenerateLiquibaseCompositeTypeElementTreeNavigationItem();
            generateLiquibaseCompositeTypeElementTreeNavigationItem.CompositeTypeElementTreeNavigationItem = compositeTypeElementTreeNavigationItem;
            generateLiquibaseCompositeTypeElementTreeNavigationItem.Generate = true;
            return generateLiquibaseCompositeTypeElementTreeNavigationItem;
        }
    }
}
