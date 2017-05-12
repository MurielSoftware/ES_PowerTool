using Desktop.Shared.Core.Navigations;
using Desktop.Ui.Core.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.Ui.Core.Controls
{
    /// <summary>
    /// Interaction logic for BaseTreeView.xaml
    /// </summary>
    public partial class BaseTreeView : TreeView
    {


        private static readonly PropertyInfo IsSelectionChangeActiveProperty = typeof(TreeView).GetProperty("IsSelectionChangeActive", BindingFlags.NonPublic | BindingFlags.Instance);

        //public static readonly DependencyProperty DropAdapterProperty = DependencyProperty.RegisterAttached("DropHandler", typeof(BaseDropAdapter), typeof(BaseTreeView), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(List<TreeNavigationItem>), typeof(BaseTreeView), new FrameworkPropertyMetadata(new List<TreeNavigationItem>(), FrameworkPropertyMetadataOptions.None));

        private BaseTreeModelView _baseTreeModelView;

        //public BaseDropAdapter DropAdapter
        //{
        //    get { return (BaseDropAdapter)GetValue(DropAdapterProperty); }
        //    set { SetValue(DropAdapterProperty, value); }
        //}

        public List<TreeNavigationItem> SelectedItems
        {
            get { return (List<TreeNavigationItem>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public BaseTreeView()
        {
            InitializeComponent();
        }
        private void TreeView_Loaded(object sender, RoutedEventArgs e)
        {
            _baseTreeModelView = (BaseTreeModelView)DataContext;
        }

        private void TreeView_Expanded(object sender, RoutedEventArgs e)
        {
            TreeNavigationItem treeNavigationItem = (e.OriginalSource as TreeViewItem).Header as TreeNavigationItem;
            _baseTreeModelView.LoadChildren(treeNavigationItem);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _baseTreeModelView.SelectionChanged(e.NewValue);
        }

        private void treeView_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!(e.Source is TreeView))
            {
                return;
            }
            TreeView sourceTreeView = e.Source as TreeView;
            if (sourceTreeView.SelectedItem == null || !(sourceTreeView.SelectedItem is TreeNavigationItem))
            {
                return;
            }
            _baseTreeModelView.SelectionChanged(sourceTreeView.SelectedItem);
        }

        private void StackPanel_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            UIElement ClickedItem = VisualTreeHelper.GetParent(e.OriginalSource as UIElement) as UIElement;
            TreeViewItem treeViewItem = GetTreeViewItem(ClickedItem);
            treeViewItem.Focus();
        }

        private void treeView_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    if (DropAdapter == null)
            //    {
            //        return;
            //    }
            //    if (!(e.Source is BaseTreeView))
            //    {
            //        return;
            //    }
            //    if (treeView.SelectedItem == null)
            //    {
            //        return;
            //    }
            //    TreeNavigationItem selectedTreeNavigationItem = treeView.SelectedItem as TreeNavigationItem;
            //    if (!DropAdapter.DragStart(e, selectedTreeNavigationItem))
            //    {
            //        return;
            //    }
            //    TreeViewItem treeViewItem = GetTreeViewItemAt(e.GetPosition(treeView));
            //    //_dragAdorner = new DragAdorner(treeViewItem, treeViewItem.DesiredSize, e.GetPosition(treeView));
            //    DragDrop.DoDragDrop(treeView, treeView.SelectedItem, DragDropEffects.All);
            //    //_dragAdorner.Detach();
            //}
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            //if (DropAdapter == null)
            //{
            //    return;
            //}
            //TreeNavigationItem targetTreeNavigationItem = GetTreeNavigationItemAt(e.GetPosition(treeView));
            //TreeNavigationItem draggedTreeNavigationItem = GetDataFromDragEventArgs(e);
            ////      _dragAdorner.UpdatePosition(e.GetPosition(treeView));
            //DropAdapter.DragOver(e, draggedTreeNavigationItem, targetTreeNavigationItem);
            //e.Handled = true;
        }

        private void treeView_Drop(object sender, DragEventArgs e)
        {
            //if (DropAdapter == null)
            //{
            //    return;
            //}
            //TreeNavigationItem targetTreeNavigationItem = GetTreeNavigationItemAt(e.GetPosition(treeView));
            //TreeNavigationItem draggedTreeNavigationItem = GetDataFromDragEventArgs(e);
            //DropAdapter.Drop(e, draggedTreeNavigationItem, targetTreeNavigationItem);
        }


        private void treeView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsFocused)
            {
                return;
            }
            if (GetTreeNavigationItemAt(e.GetPosition(treeView)) != null)
            {
                return;
            }
            SelectedItems.ForEach(item => item.IsSelected = false);
            SelectedItems.Clear();
        }

        private TreeNavigationItem GetTreeNavigationItemAt(Point location)
        {
            TreeViewItem treeViewItem = GetTreeViewItemAt(location);
            if (treeViewItem == null)
            {
                return null;
            }
            return treeViewItem.Header as TreeNavigationItem;
        }

        private TreeViewItem GetTreeViewItemAt(Point location)
        {
            UIElement dropNode = VisualTreeHelper.HitTest(treeView, location).VisualHit as UIElement;
            if (dropNode == null)
            {
                return null;
            }
            TreeViewItem treeViewItem = GetTreeViewItem(dropNode);
            if (treeViewItem == null)
            {
                return null;
            }
            return treeViewItem;
        }

        private TreeViewItem GetTreeViewItem(UIElement element)
        {
            while ((element != null) && !(element is TreeViewItem))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
            }
            return element as TreeViewItem;
        }

        private TreeNavigationItem GetDataFromDragEventArgs(DragEventArgs e)
        {
            //Type[] treeNavigationItemTypes = { typeof(TreeNavigationItem), typeof(ResourceTreeNavigationItem), typeof(FolderTreeNavigationItem) };
            //foreach (Type treeNavigationItemType in treeNavigationItemTypes)
            //{
            //    if (e.Data.GetDataPresent(treeNavigationItemType))
            //    {
            //        return e.Data.GetData(treeNavigationItemType) as TreeNavigationItem;
            //    }
            //}
            return null;
        }

        private void AllowMultiSelection(TreeView treeView)
        {
            if (IsSelectionChangeActiveProperty == null) return;
            treeView.SelectedItemChanged += (a, b) =>
            {
                TreeNavigationItem treeNavigationItem = /*treeView.SelectedItem*/ b.NewValue as TreeNavigationItem;
                if (treeNavigationItem == null)
                {
                    return;
                }

                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    object isSelectionChangeActive = IsSelectionChangeActiveProperty.GetValue(treeView, null);
                    IsSelectionChangeActiveProperty.SetValue(treeView, true, null);
                    SelectedItems.ForEach(item => item.IsSelected = true);
                    IsSelectionChangeActiveProperty.SetValue(treeView, isSelectionChangeActive, null);
                }
                else
                {
                    SelectedItems.ForEach(item => item.IsSelected = (item == treeNavigationItem));
                    SelectedItems.Clear();
                }

                if (!SelectedItems.Contains(treeNavigationItem))
                {
                    SelectedItems.Add(treeNavigationItem);
                }
                else
                {
                    treeNavigationItem.IsSelected = false;
                    SelectedItems.Remove(treeNavigationItem);
                }
            };
        }
    }
}
