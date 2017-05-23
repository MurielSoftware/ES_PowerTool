using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Services
{
    public interface ICompositeTypeNavigationService : INavigationService
    {
        List<TreeNavigationItem> GetAllDerivableCompositeTypes();
        List<TreeNavigationItem> GetAllTypes();
        List<TreeNavigationItem> GetAllCompositeTypes();
        List<TreeNavigationItem> GetAllPrimitiveTypes();
    }
}
