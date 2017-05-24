using Desktop.Shared.Core.Services;
using System.Collections.Generic;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Data.DAL;
using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Data.DAL.OOE.Types;
using ES_PowerTool.Data.DAL.OOE.Elements;
using ES_PowerTool.Data.DAL.OOE;
using ES_PowerTool.Shared.Services.OOE.Types;

namespace ES_PowerTool.Data.BAL.OOE.Types
{
    public class CompositeTypeNavigationService : BaseNavigationService, ICompositeTypeNavigationService
    {
        private PrimitiveTypeNavigationRepository _primitiveTypeNavigationRepository;
        private CompositeTypeNavigationRepository _compositeTypeNavigationRepository;
        private CompositeTypeElementNavigationRepository _compositeTypeElementNavigationRepository;
        private FolderNavigationRepository _folderNavigationRepository;
        private ProjectNavigationRepository _projectNavigationRepository;

        public CompositeTypeNavigationService(Connection connection) 
            : base(connection)
        {
            _primitiveTypeNavigationRepository = new PrimitiveTypeNavigationRepository(connection);
            _compositeTypeNavigationRepository = new CompositeTypeNavigationRepository(connection);
            _compositeTypeElementNavigationRepository = new CompositeTypeElementNavigationRepository(connection);
            _folderNavigationRepository = new FolderNavigationRepository(connection);
            _projectNavigationRepository = new ProjectNavigationRepository(connection);
        }

        public List<TreeNavigationItem> GetAllCompositeTypes()
        {
            return _compositeTypeNavigationRepository.FindAllTypes();
        }

        public List<TreeNavigationItem> GetAllPrimitiveTypes()
        {
            return _primitiveTypeNavigationRepository.FindAllTypes();
        }

        public List<TreeNavigationItem> GetAllTypes()
        {
            List<TreeNavigationItem> typesTreeNavigationItems = new List<TreeNavigationItem>();
            typesTreeNavigationItems.AddRange(GetAllCompositeTypes());
            typesTreeNavigationItems.AddRange(GetAllPrimitiveTypes());
            return typesTreeNavigationItems;
        }

        public List<TreeNavigationItem> GetAllDerivableCompositeTypes()
        {
            return _compositeTypeNavigationRepository.FindAllDerivableTypes();
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
                    if (parentTreeNavigationItem.Name == "PrimitiveTypes")
                    {
                        children.AddRange(_folderNavigationRepository.FindChildren(parentTreeNavigationItem.Id));
                        children.AddRange(_primitiveTypeNavigationRepository.FindChildren(parentTreeNavigationItem.Id));
                    }
                    else
                    {
                        children.AddRange(_folderNavigationRepository.FindChildren(parentTreeNavigationItem.Id));
                        children.AddRange(_compositeTypeNavigationRepository.FindChildren(parentTreeNavigationItem.Id));
                    }
                    break;
                case NavigationType.COMPOSITE_TYPE:
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
                case NavigationType.COMPOSITE_TYPE:
                    updatedTreeNavigationItem = _compositeTypeNavigationRepository.FindSpecific(treeNavigationItem.Id);
                    break;
                case NavigationType.TYPE_ELEMENT:
                    updatedTreeNavigationItem = _compositeTypeElementNavigationRepository.FindSpecific(treeNavigationItem.Id);
                    break;
            }
            return updatedTreeNavigationItem;
        }
    }
}
