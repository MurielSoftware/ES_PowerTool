using Desktop.App.Core.Events.Selection;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core.Services;
using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.App.Core.Events.Publishing;

namespace Desktop.App.Core.ModelViews
{
    public abstract class BaseDetailsTreeModelView : BaseTreeModelView, ISelectionChangeListener
    {
        private TreeNavigationItem _masterNavigationItem;

        public BaseDetailsTreeModelView(string modelViewId) 
            : base(modelViewId)
        {
            _masterSelectionChangeService.AddSelectionChangeListener(this);
            _service = CreateNavigationService();
        }

        public TreeNavigationItem GetMasterNavigationItem()
        {
            return _masterNavigationItem;
        }

        public async void OnSelectionChange(object selection)
        {
            if(!(selection is TreeNavigationItem))
            {
                return;
            }
            _masterNavigationItem = (TreeNavigationItem)selection;
            Roots = new ObservableCollection<TreeNavigationItem>();
            Roots.Add(new TreeNavigationItem(Guid.Empty, ResourceUtils.GetMessage(MessageKeyConstants.LABEL_LOADING), NavigationType.FOLDER));
            OnPropertyChanged(() => Roots);

            Roots = new ObservableCollection<TreeNavigationItem>(await DoLoadRoots());
            OnPropertyChanged(() => Roots);
        }

        protected override void OnCreate(PublishEvent publishEvent)
        {
            TreeNavigationItem parentTreeNavigationItem = Find(Roots, publishEvent.AffectedObjectId);
            if (parentTreeNavigationItem == null)
            {
                return;
            }
            parentTreeNavigationItem.SetChildren(_service.GetChildren(MasterNavigationContext.CreateMasterNavigationContext(_masterNavigationItem), parentTreeNavigationItem));
            parentTreeNavigationItem.IsExpanded = true;

            TreeNavigationItem affectedTreeNavigationItem = Find(Roots, publishEvent.AffectedObjectId);
            if (affectedTreeNavigationItem != null)
            {
                affectedTreeNavigationItem.IsSelected = true;
            }
        }

        protected override async Task<List<TreeNavigationItem>> DoLoadRoots()
        {
            if(_masterNavigationItem == null)
            {
                return new List<TreeNavigationItem>();
            }
            return await Task.Run(() =>
            {
                List<TreeNavigationItem> roots = _service.GetRoots(MasterNavigationContext.CreateMasterNavigationContext(_masterNavigationItem));
                foreach(TreeNavigationItem root in roots)
                {
                    if(!root.HasRemoteChildren && root.Children.Count > 0)
                    {
                        root.IsExpanded = true;
                    }
                }
                return roots;
            });
        }

        protected override async Task<List<TreeNavigationItem>> DoLoadChildren(TreeNavigationItem parentTreeNavigationItem)
        {
            return await Task.Run(() => 
            {
                return _service.GetChildren(MasterNavigationContext.CreateMasterNavigationContext(_masterNavigationItem), parentTreeNavigationItem);
            });
        }
    }
}
