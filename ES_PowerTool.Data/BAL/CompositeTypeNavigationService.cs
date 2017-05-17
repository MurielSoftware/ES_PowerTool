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
        private ProjectNavigationRepository _projectNavigationRepository;

        public CompositeTypeNavigationService(Connection connection) 
            : base(connection)
        {
            _compositeTypeNavigationRepository = new CompositeTypeNavigationRepository(connection);
            _compositeTypeElementNavigationRepository = new CompositeTypeElementNavigationRepository(connection);
            _folderNavigationRepository = new FolderNavigationRepository(connection);
            _projectNavigationRepository = new ProjectNavigationRepository(connection);
        }

        public List<TreeNavigationItem> GetAllDerivableCompositeTypes()
        {
            return _compositeTypeNavigationRepository.FindAllDerivable();
        }

        public List<TreeNavigationItem> GetChildren(NavigationContext navigationContext, TreeNavigationItem parentTreeNavigationItem)
        {
            List<TreeNavigationItem> children = new List<TreeNavigationItem>();
            switch(parentTreeNavigationItem.Type)
            {
                case NavigationType.PROJECT:
                    children.AddRange(_folderNavigationRepository.FindRoots(parentTreeNavigationItem.Id));
                    break;
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
            List<TreeNavigationItem> roots = _projectNavigationRepository.FindRoots();
            ExtendTreeNavigationItems(roots, null);
            return roots;
        }

        public TreeNavigationItem Reload(TreeNavigationItem treeNavigationItem)
        {
            TreeNavigationItem updatedTreeNavigationItem = null;
            switch(treeNavigationItem.Type)
            {
                case NavigationType.PROJECT:
                    updatedTreeNavigationItem = _projectNavigationRepository.FindSpecific(treeNavigationItem.Id);
                    break;
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
