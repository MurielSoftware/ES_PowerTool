using Desktop.Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Shared.Services;
using Desktop.Shared.Navigations;
using ES_PowerTool.Data.DAL;

namespace ES_PowerTool.Data.BAL
{
    public class CompositeTypeNavigationService : BaseService, ICompositeTypeNavigationService
    {
        private CompositeTypeNavigationRepository _compositeTypeNavigationRepository;
        private CompositeTypeElementNavigationRepository _compositeTypeElementNavigationRepository;
        private FolderNavigationRepository _folderNavigationRepository;

        public CompositeTypeNavigationService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _compositeTypeNavigationRepository = new CompositeTypeNavigationRepository(unitOfWork);
            _compositeTypeElementNavigationRepository = new CompositeTypeElementNavigationRepository(unitOfWork);
            _folderNavigationRepository = new FolderNavigationRepository(unitOfWork);
        }

        public List<TreeNavigationItem> GetChildren(NavigationContext navigationContext, TreeNavigationItem parentTreeNavigationItem)
        {
            throw new NotImplementedException();
        }

        public List<TreeNavigationItem> GetRoots(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public TreeNavigationItem Reload(TreeNavigationItem treeNavigationItem)
        {
            throw new NotImplementedException();
        }
    }
}
