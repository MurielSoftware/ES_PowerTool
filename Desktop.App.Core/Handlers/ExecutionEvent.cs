using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Handlers
{
    public class ExecutionEvent
    {
        private List<TreeNavigationItem> _selectedTreeNavigationItems;
        private TreeNavigationItem _masterTreeNavigationItem; 
        //public BaseDto Dto { get; set; }

        private ExecutionEvent()
        {
        }

        private ExecutionEvent(List<TreeNavigationItem> selectedtreeNavigationItems)
        {
            _selectedTreeNavigationItems = selectedtreeNavigationItems;
        }

        private ExecutionEvent(List<TreeNavigationItem> selectedtreeNavigationItems, TreeNavigationItem masterTreeNavigationItem)
            : this(selectedtreeNavigationItems)
        {
            _masterTreeNavigationItem = masterTreeNavigationItem;
        }

        //private ExecutionEvent(List<TreeNavigationItem> treeNavigationItems, BaseDto dto)
        //    : this(treeNavigationItems)
        //{
        //    Dto = dto;
        //}

        ////private ExecutionEvent(List<TreeNavigationItem> treeNavigationItems, ResourceType resourceType)
        ////    : this(treeNavigationItems)
        ////{
        ////    ResourceType = resourceType;
        ////}

        public TreeNavigationItem GetFirstSelectedTreeNavigationItem()
        {
            return _selectedTreeNavigationItems.First();
        }

        public TreeNavigationItem GetMasterTreeNavigationItem()
        {
            return _masterTreeNavigationItem;
        }

        public List<TreeNavigationItem> GetSelectedTreeNavigationItems()
        {
            return _selectedTreeNavigationItems;
        }

        public static ExecutionEvent Create()
        {
            return new ExecutionEvent();
        }

        public static ExecutionEvent Create(List<TreeNavigationItem> selectedTreeNavigationItems)
        {
            return new ExecutionEvent(selectedTreeNavigationItems);
        }

        public static ExecutionEvent Create(List<TreeNavigationItem> selectedTreeNavigationItems, TreeNavigationItem masterTreeNavigationItem)
        {
            return new ExecutionEvent(selectedTreeNavigationItems, masterTreeNavigationItem);
        }

        public static ExecutionEvent Create(TreeNavigationItem selectedTreeNavigationItem)
        {
            return new ExecutionEvent(new List<TreeNavigationItem>() { selectedTreeNavigationItem });
        }

        public static ExecutionEvent Create(TreeNavigationItem selectedTreeNavigationItem, TreeNavigationItem masterTreeNavigationItem)
        {
            return new ExecutionEvent(new List<TreeNavigationItem>() { selectedTreeNavigationItem }, masterTreeNavigationItem);
        }

        //public static ExecutionEvent Create(List<TreeNavigationItem> treeNavigationItems, ResourceType resourceType)
        //{
        //    return new ExecutionEvent(treeNavigationItems, resourceType);
        //}

        //public static ExecutionEvent Create(List<TreeNavigationItem> treeNavigationItems, BaseDto dto)
        //{
        //    return new ExecutionEvent(treeNavigationItems, dto);
        //}
    }
}
