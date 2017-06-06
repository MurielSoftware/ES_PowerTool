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
using Desktop.Shared.Core.Context;

namespace Desktop.App.Core.ModelViews
{
    public abstract class BaseTreeModelView : BaseModelView, IPublishListener, IServerChangedListener
    {
        protected INavigationService _service;

        public ObservableCollection<TreeNavigationItem> Roots { get; protected set; }

        public BaseTreeModelView(string modelViewId)
            : base(modelViewId)
        {
            Publisher.GetInstance().AddListener(this);
            Publisher.GetInstance().AddServerListener(this);
            Roots = new ObservableCollection<TreeNavigationItem>();
        }

        protected abstract INavigationService CreateNavigationService();

        public async void OnServerSwitched(object obj)
        {
            if(obj == null)
            {
                Roots.Clear();
                OnPropertyChanged(() => Roots);
                return;
            }
            _service = CreateNavigationService();
            Roots = new ObservableCollection<TreeNavigationItem>();
            Roots.Add(new TreeNavigationItem(Guid.Empty, ResourceUtils.GetMessage(MessageKeyConstants.LABEL_LOADING), NavigationType.FOLDER));
            OnPropertyChanged(() => Roots);

            Roots = new ObservableCollection<TreeNavigationItem>(await DoLoadRoots());
            OnPropertyChanged(() => Roots);
        }

        protected virtual async Task<List<TreeNavigationItem>> DoLoadRoots()
        {
            return await Task.Run(() =>
            {
                return _service.GetRoots(NavigationContext.CreateNavigationContext());
            });
        }

        public async void LoadChildren(TreeNavigationItem parentTreeNavigationItem)
        {
            if (parentTreeNavigationItem == null)
            {
                return;
            }

            if (parentTreeNavigationItem.HasRemoteChildren)
            {
                parentTreeNavigationItem.HasRemoteChildren = false;
                parentTreeNavigationItem.SetChildren(await DoLoadChildren(parentTreeNavigationItem));
            }
        }

        protected virtual async Task<List<TreeNavigationItem>> DoLoadChildren(TreeNavigationItem parentTreeNavigationItem)
        {
            return await Task.Run(() => 
            {
                return _service.GetChildren(NavigationContext.CreateNavigationContext(), parentTreeNavigationItem);
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

        protected virtual void OnCreate(PublishEvent publishEvent)
        {
            TreeNavigationItem parentTreeNavigationItem = Find(Roots, publishEvent.ParentObjectId.Value);
            if (parentTreeNavigationItem == null)
            {
                return;
            }
            parentTreeNavigationItem.SetChildren(_service.GetChildren(NavigationContext.CreateNavigationContext(), parentTreeNavigationItem));
            parentTreeNavigationItem.IsExpanded = true;
            TreeNavigationItem affectedTreeNavigationItem = Find(Roots, publishEvent.AffectedObjectId);
            if (affectedTreeNavigationItem != null)
            {
                affectedTreeNavigationItem.IsSelected = true;
            }
        }

        protected virtual void OnUpdate(PublishEvent publishEvent)
        {
            TreeNavigationItem treeNavigationItem = Find(Roots, publishEvent.AffectedObjectId);
            if (treeNavigationItem == null)
            {
                return;
            }
            TreeNavigationItem parentTreeNavigationItem = treeNavigationItem.Parent;
            if (parentTreeNavigationItem == null)
            {
                DoUpdate(Roots, parentTreeNavigationItem, treeNavigationItem);
            }
            else
            {
                DoUpdate(parentTreeNavigationItem.Children, parentTreeNavigationItem, treeNavigationItem);
            }
        }

        private void DoUpdate(IList<TreeNavigationItem> children, TreeNavigationItem parentTreeNavigationItem, TreeNavigationItem originalTreeNavigationItem)
        {
            int indexOfUpdatedTreeNavigationItem = children.IndexOf(originalTreeNavigationItem);
            children.RemoveAt(indexOfUpdatedTreeNavigationItem);
            TreeNavigationItem updatedTreeNavigationItem = _service.Reload(originalTreeNavigationItem);
            updatedTreeNavigationItem.Parent = parentTreeNavigationItem;
            updatedTreeNavigationItem.HasRemoteChildren = originalTreeNavigationItem.HasRemoteChildren;
            updatedTreeNavigationItem.Children = originalTreeNavigationItem.Children;

            if (!updatedTreeNavigationItem.HasRemoteChildren && updatedTreeNavigationItem.Children.Count > 0)
            {
                updatedTreeNavigationItem.IsExpanded = true;
            }
            children.Insert(indexOfUpdatedTreeNavigationItem, updatedTreeNavigationItem);
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
            _masterSelectionChangeService.SelectionChanged(GetModelViewId(), selection);
        }

        protected TreeNavigationItem Find(ObservableCollection<TreeNavigationItem> treeNavigationItems, Guid id)
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
