using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core.Services;
using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.Events.Selection;
using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.ModelViews
{
    public abstract class BaseTreeModelView : BaseModelView, IPublishListener, IServerChangedListener
    {
        protected INavigationService _service;

        public ObservableCollection<TreeNavigationItem> Roots { get; private set; }

        public BaseTreeModelView(string modelViewId)
            : base(modelViewId)
        {
            Publisher.GetInstance().AddListener(this);
            Publisher.GetInstance().AddServerListener(this);
            Roots = new ObservableCollection<TreeNavigationItem>();
        }

        public abstract void SetService();

        public async void OnServerSwitched(object obj)
        {
            //if (obj == null)
            //{
            //    Roots.Clear();
            //    OnPropertyChanged(() => Roots);
            //    return;
            //}
            SetService();

            Roots = new ObservableCollection<TreeNavigationItem>();
            Roots.Add(new TreeNavigationItem(Guid.Empty, ResourceUtils.GetMessage(MessageKeyConstants.LABEL_LOADING), NavigationType.FOLDER));
            OnPropertyChanged(() => Roots);

            Roots = new ObservableCollection<TreeNavigationItem>(await DoLoadRoots());
            OnPropertyChanged(() => Roots);
        }

        private async Task<List<TreeNavigationItem>> DoLoadRoots()
        {
            return await Task.Run(() =>
            {
                return _service.GetRoots(NavigationContext.CreateNavigationContext());
            });
        }

        public void OnEvent(PublishEvent publishEvent)
        {
            switch (publishEvent.EventType)
            {
                case EventType.CREATE:
                    OnCreate(publishEvent);
                    break;
                case EventType.UPDATE:
                    OnUpdate(publishEvent);
                    break;
                case EventType.DELETE:
                    OnDelete(publishEvent);
                    break;
            }
        }

        public async void LoadChildren(TreeNavigationItem parentTreeNavigationItem)
        {
            if (parentTreeNavigationItem.HasRemoteChildren)
            {
                parentTreeNavigationItem.HasRemoteChildren = false;
                parentTreeNavigationItem.SetChildren(await DoLoadChildren(parentTreeNavigationItem));
            }
        }

        private async Task<List<TreeNavigationItem>> DoLoadChildren(TreeNavigationItem parentTreeNavigationItem)
        {
            return await Task.Run(() => {
                return _service.GetChildren(NavigationContext.CreateNavigationContext(), parentTreeNavigationItem);
            });
        }

        protected virtual void OnCreate(PublishEvent publishEvent)
        {
            TreeNavigationItem parentTreeNavigationItem = Find(Roots, publishEvent.ParentObjectId.Value);
            if (parentTreeNavigationItem == null)
            {
                return;
            }
            parentTreeNavigationItem.SetChildren(_service.GetChildren(NavigationContext.CreateNavigationContext(), parentTreeNavigationItem));
            parentTreeNavigationItem.IsExpanded = true;
            Find(Roots, publishEvent.AffectedObjectId).IsSelected = true;
        }

        protected virtual void OnUpdate(PublishEvent publishEvent)
        {
            TreeNavigationItem treeNavigationItem = Find(Roots, publishEvent.AffectedObjectId);
            if (treeNavigationItem == null)
            {
                return;
            }
            TreeNavigationItem updatedTreeNavigationItem = _service.Reload(treeNavigationItem);
            TreeNavigationItem parentTreeNavigationItem = treeNavigationItem.Parent;
            if (parentTreeNavigationItem == null)
            {
                int indexOfUpdatedTreeNavigationItem = Roots.IndexOf(treeNavigationItem);
                Roots.RemoveAt(indexOfUpdatedTreeNavigationItem);
                Roots.Insert(indexOfUpdatedTreeNavigationItem, updatedTreeNavigationItem);
            }
            else
            {
                int indexOfUpdatedTreeNavigationItem = parentTreeNavigationItem.Children.IndexOf(treeNavigationItem);
                //parentTreeNavigationItem.Children[indexOfUpdatedTreeNavigationItem] = updatedTreeNavigationItem;
                parentTreeNavigationItem.Children.RemoveAt(indexOfUpdatedTreeNavigationItem);
                parentTreeNavigationItem.Children.Insert(indexOfUpdatedTreeNavigationItem, updatedTreeNavigationItem);
            }
            updatedTreeNavigationItem.HasRemoteChildren = treeNavigationItem.HasRemoteChildren;
            updatedTreeNavigationItem.Children = treeNavigationItem.Children;

            parentTreeNavigationItem.IsSelected = false;
            updatedTreeNavigationItem.IsSelected = true;
        }

        protected virtual void OnDelete(PublishEvent publishEvent)
        {
            TreeNavigationItem treeNavigationItem = Find(Roots, publishEvent.AffectedObjectId);
            if (treeNavigationItem == null)
            {
                return;
            }
            if (treeNavigationItem.GetParentId() == null)
            {
                Roots.Remove(treeNavigationItem);
            }
            else
            {
                TreeNavigationItem parentTreeNavigationItem = treeNavigationItem.Parent;
                parentTreeNavigationItem.Children.Remove(treeNavigationItem);
            }
        }

        public virtual void SelectionChanged(object selection)
        {
            //_masterSelectionChangeService.SelectionChanged(GetModelViewId(), selection);
        }

        private TreeNavigationItem Find(ObservableCollection<TreeNavigationItem> treeNavigationItems, Guid id)
        {
            foreach (TreeNavigationItem treeNavigationItem in treeNavigationItems)
            {
                if (id.Equals(treeNavigationItem.Id))
                {
                    return treeNavigationItem;
                }
                TreeNavigationItem foundTreeNavigationItem = Find(treeNavigationItem.Children, id);
                if (foundTreeNavigationItem != null)
                {
                    return foundTreeNavigationItem;
                }
            }
            return null;
        }
    }
}
