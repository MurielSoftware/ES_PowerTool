using Desktop.Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Shared.Services;
using ES_PowerTool.Data.DAL;
using Desktop.Shared.Core.Navigations;

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
            List<TreeNavigationItem> children = new List<TreeNavigationItem>();
            switch(parentTreeNavigationItem.Type)
            {
                case NavigationType.FOLDER:
                    children.AddRange(_folderNavigationRepository.FindChildren(parentTreeNavigationItem.Id));
                    children.AddRange(_compositeTypeNavigationRepository.FindChildren(parentTreeNavigationItem.Id));
                    break;
                case NavigationType.TYPE:
                    children.AddRange(_compositeTypeElementNavigationRepository.FindChildren(parentTreeNavigationItem.Id));
                    break;
            }
            ExtendTreeNavigationItems(children, parentTreeNavigationItem);
            return children;
        }

        public List<TreeNavigationItem> GetRoots(NavigationContext navigationContext)
        {
            List<TreeNavigationItem> roots = _folderNavigationRepository.FindRoots();
            ExtendTreeNavigationItems(roots, null);
            return roots;
        }

        public TreeNavigationItem Reload(TreeNavigationItem treeNavigationItem)
        {
            TreeNavigationItem updatedTreeNavigationItem = null;
            switch(treeNavigationItem.Type)
            {
                case NavigationType.FOLDER:
                    updatedTreeNavigationItem = _folderNavigationRepository.FindSpecific(treeNavigationItem.Id);
                    break;
                case NavigationType.TYPE:
                    updatedTreeNavigationItem = _compositeTypeNavigationRepository.FindSpecific(treeNavigationItem.Id);
                    break;
                case NavigationType.TYPE_ELEMENT:
                    updatedTreeNavigationItem = _compositeTypeElementNavigationRepository.FindSpecific(treeNavigationItem.Id);
                    break;
            }
            return updatedTreeNavigationItem;
        }

        protected virtual void ExtendTreeNavigationItems(List<TreeNavigationItem> treeNavigationItems, TreeNavigationItem parentTreeNavigationItem)
        {
            TreeNavigationItem loadingTreeNavigationItem = new TreeNavigationItem(Guid.Empty, "Loading...", NavigationType.FOLDER);

            foreach (TreeNavigationItem treeNavigationItem in treeNavigationItems)
            {
                if (treeNavigationItem.HasRemoteChildren)
                {
                    treeNavigationItem.Children.Add(loadingTreeNavigationItem);
                }
                if (parentTreeNavigationItem != null)
                {
                    treeNavigationItem.Parent = parentTreeNavigationItem;
                }
            }
        }
    }
}
