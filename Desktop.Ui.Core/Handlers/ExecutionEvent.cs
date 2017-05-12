using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Ui.Core.Handlers
{
    public class ExecutionEvent
    {
        public List<TreeNavigationItem> _treeNavigationItems;// { get; private set; }
        public BaseDto Dto { get; set; }
        //public ResourceType ResourceType { get; private set; }

        private ExecutionEvent()
        {
        }

        private ExecutionEvent(List<TreeNavigationItem> treeNavigationItems)
        {
            _treeNavigationItems = treeNavigationItems;
        }

        private ExecutionEvent(List<TreeNavigationItem> treeNavigationItems, BaseDto dto)
            : this(treeNavigationItems)
        {
            Dto = dto;
        }

        //private ExecutionEvent(List<TreeNavigationItem> treeNavigationItems, ResourceType resourceType)
        //    : this(treeNavigationItems)
        //{
        //    ResourceType = resourceType;
        //}

        public TreeNavigationItem GetFirstTreeNavigationItem()
        {
            return _treeNavigationItems.First();
        }

        public List<TreeNavigationItem> GetTreeNavigationItems()
        {
            return _treeNavigationItems;
        }

        public static ExecutionEvent Create()
        {
            return new ExecutionEvent();
        }

        public static ExecutionEvent Create(List<TreeNavigationItem> treeNavigationItems)
        {
            return new ExecutionEvent(treeNavigationItems);
        }

        //public static ExecutionEvent Create(List<TreeNavigationItem> treeNavigationItems, ResourceType resourceType)
        //{
        //    return new ExecutionEvent(treeNavigationItems, resourceType);
        //}

        public static ExecutionEvent Create(List<TreeNavigationItem> treeNavigationItems, BaseDto dto)
        {
            return new ExecutionEvent(treeNavigationItems, dto);
        }
    }
}
