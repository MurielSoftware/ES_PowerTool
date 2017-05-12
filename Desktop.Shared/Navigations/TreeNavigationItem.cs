using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Navigations
{
    public class TreeNavigationItem : INotifyPropertyChanged
    {
        private bool _isSelected;
        private bool _isExpanded;

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded = value;
                OnPropertyChanged(() => IsExpanded);
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged(() => IsSelected);
            }
        }

        public Uri Image
        {
            get { return NavigationTypeToImage.NAVIGATION_TYPE_TO_IMAGE[Type]; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual NavigationType Type { get; set; }
        public virtual TreeNavigationItem Parent { get; set; }
        public virtual ObservableCollection<TreeNavigationItem> Children { get; set; }
        public virtual bool HasRemoteChildren { get; set; }

        public TreeNavigationItem()
        {
            Children = new ObservableCollection<TreeNavigationItem>();
        }

        public TreeNavigationItem(Guid id, string name, NavigationType type)
            : this()
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public TreeNavigationItem(Guid id, string name, NavigationType type, bool remote)
            : this(id, name, type)
        {
            HasRemoteChildren = remote;
        }

        public void SetChildren(List<TreeNavigationItem> children)
        {
            Children = new ObservableCollection<TreeNavigationItem>(children);
            OnPropertyChanged(() => Children);
        }

        public virtual Guid? GetParentId()
        {
            if (Parent == null)
            {
                return null;
            }
            return Parent.Id;
        }

        private void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            var property = (MemberExpression)expression.Body;
            OnPropertyChanged(property.Member.Name);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is TreeNavigationItem))
            {
                return false;
            }

            TreeNavigationItem rsd = obj as TreeNavigationItem;
            return Id.Equals(rsd.Id);
        }

        public string GetPathToRoot()
        {
            StringBuilder sb = new StringBuilder();
            TreeNavigationItem currentTreeNavigationItem = this;
            while (currentTreeNavigationItem != null)
            {
                sb.Insert(0, "/" + currentTreeNavigationItem.Name);
                currentTreeNavigationItem = currentTreeNavigationItem.Parent;
            }
            return sb.ToString();
        }

        public List<Guid> GetPathIdsToRoot()
        {
            List<Guid> ids = new List<Guid>();
            TreeNavigationItem currentTreeNavigationItem = this;
            while (currentTreeNavigationItem != null)
            {
                ids.Add(currentTreeNavigationItem.Id);
                currentTreeNavigationItem = currentTreeNavigationItem.Parent;
            }
            return ids;
        }

        public static ICollection<Guid> CollectIds(List<TreeNavigationItem> treeNavigationItems)
        {
            return treeNavigationItems.Where(x => x.Parent != null).Select(x => x.Id).ToList();
        }

        public static ICollection<Guid> CollectParentIds(List<TreeNavigationItem> treeNavigationItems)
        {
            return treeNavigationItems.Where(x => x.Parent != null).Select(x => x.Parent.Id).ToList();
        }

        public static Dictionary<Guid, Guid> GetIdToParentId(List<TreeNavigationItem> treeNavigationItems)
        {
            return treeNavigationItems.Where(x => x.Parent != null).ToDictionary(x => x.Id, x => x.Parent.Id);
        }

        public static bool IsSameType(List<TreeNavigationItem> treeNavigationItems)
        {
            return treeNavigationItems.Where(x => x.Type == treeNavigationItems.First().Type).Count() == treeNavigationItems.Count;
        }
    }
}
