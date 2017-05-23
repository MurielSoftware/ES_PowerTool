using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Data.DAL;
using System.Collections.ObjectModel;
using ES_PowerTool.Shared;

namespace ES_PowerTool.Data.BAL
{
    public class CompositeTypeDetailsNavigationService : BaseNavigationService, ICompositeTypeDetailsNavigationService
    {
        private PresetNavigationRepository _presetNavigationRepository;

        public CompositeTypeDetailsNavigationService(Connection connection) 
            : base(connection)
        {
            _presetNavigationRepository = new PresetNavigationRepository(connection);
        }

        public List<TreeNavigationItem> GetRoots(NavigationContext navigationContext)
        {
            MasterNavigationContext masterNavigationContext = (MasterNavigationContext)navigationContext;
            return CreateVirtualFolders(masterNavigationContext.GetMasterNavigationItem().Id);
        }

        public List<TreeNavigationItem> GetChildren(NavigationContext navigationContext, TreeNavigationItem parentTreeNavigationItem)
        {
            MasterNavigationContext masterNavigationContext = (MasterNavigationContext)navigationContext;
            List<TreeNavigationItem> children = new List<TreeNavigationItem>();
            switch (parentTreeNavigationItem.Type)
            {
                case NavigationType.FOLDER:
                    children.AddRange(GetChildrenToFolder(parentTreeNavigationItem.Id, masterNavigationContext.GetMasterNavigationItem().Id));
                    break;
            }
            ExtendTreeNavigationItems(children, parentTreeNavigationItem);
            return children;
        }

        public TreeNavigationItem Reload(TreeNavigationItem treeNavigationItem)
        {
            TreeNavigationItem updatedTreeNavigationItem = null;
            switch (treeNavigationItem.Type)
            {
                case NavigationType.PRESET:
                    updatedTreeNavigationItem = _presetNavigationRepository.FindSpecificPreset(treeNavigationItem.Id);
                    break;
            }
            return updatedTreeNavigationItem;
        }

        private List<TreeNavigationItem> CreateVirtualFolders(Guid compositeTypeId)
        {
            List<TreeNavigationItem> roots = new List<TreeNavigationItem>();
            TreeNavigationItem presetRoot = new TreeNavigationItem(IdConstants.PRESET_FOLDER_ID, "Presets", NavigationType.FOLDER);
            List<TreeNavigationItem> presets = GetChildrenToFolder(presetRoot.Id, compositeTypeId);
            ExtendTreeNavigationItems(presets, presetRoot);
            presetRoot.Children = new ObservableCollection<TreeNavigationItem>(presets);
            roots.Add(presetRoot);
            return roots;
        }

        private List<TreeNavigationItem> GetChildrenToFolder(Guid parentId, Guid masterNavigationItemId)
        {
            if(IdConstants.PRESET_FOLDER_ID.Equals(parentId))
            {
                return _presetNavigationRepository.FindPresetsToCompositeType(masterNavigationItemId).Cast<TreeNavigationItem>().ToList();
            }
            return new List<TreeNavigationItem>();
        }
    }
}
