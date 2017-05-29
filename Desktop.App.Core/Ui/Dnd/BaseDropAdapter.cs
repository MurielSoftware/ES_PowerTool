using Desktop.App.Core.Ui.Dnd.DropHandlers;
using Desktop.App.Core.Ui.Dnd.Validators;
using Desktop.Shared.Core.Navigations;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Desktop.App.Core.Ui.Dnd
{
    public abstract class BaseDropAdapter
    {
        protected IList<IDragAndDropEqualityValidator> _dragAndDropEqualityValidators = new List<IDragAndDropEqualityValidator>();
        protected IList<IDropHandler> _dropHandlers = new List<IDropHandler>();

        protected TreeNavigationItem _draggedTreeNavigationItem;
        protected TreeNavigationItem _targetTreeNavigationItem;

        public BaseDropAdapter()
        {
            AddDragAndDropEqualityValidators();
            AddDropHandlers();
        }

        public virtual bool DragStart(MouseEventArgs e, TreeNavigationItem draggedTreeNavigationItem)
        {
            _draggedTreeNavigationItem = draggedTreeNavigationItem;
            return true;
        }

        public virtual void DragOver(DragEventArgs e, TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            if (!ValidateDragAndDrop(_dragAndDropEqualityValidators, draggedTreeNavigationItem, targetTreeNavigationItem))
            {
                e.Effects = DragDropEffects.None;
                return;
            }
            e.Effects = DragDropEffects.Move;
        }

        public void Drop(DragEventArgs e, TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            IDropHandler activeDropHandler = null;
            if(draggedTreeNavigationItem == null || targetTreeNavigationItem == null)
            {
                return;
            }
            if (draggedTreeNavigationItem.Id.Equals(targetTreeNavigationItem.Id))
            {
                return;
            }
            foreach (IDropHandler dropHandler in _dropHandlers)
            {
                if(dropHandler.IsActive(draggedTreeNavigationItem, targetTreeNavigationItem))
                {
                    activeDropHandler = dropHandler;
                    break;
                }
            }
            if(activeDropHandler != null)
            {
                activeDropHandler.Drop(draggedTreeNavigationItem, targetTreeNavigationItem);
            }
        }

        protected virtual void AddDragAndDropEqualityValidators()
        {
            _dragAndDropEqualityValidators.Add(new NavigationItemIsNotEmptyValidator());
            _dragAndDropEqualityValidators.Add(new DragAndTargetNotSame());
            _dragAndDropEqualityValidators.Add(new AncestorToDescendantsDropValidator());
        }

        protected abstract void AddDropHandlers();

        private bool ValidateDragAndDrop(IList<IDragAndDropValidator> dragAndDropValidators, TreeNavigationItem treeNavigationItem)
        {
            foreach (IDragAndDropValidator dragAndDropValidator in dragAndDropValidators)
            {
                if (!dragAndDropValidator.IsValid(treeNavigationItem))
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateDragAndDrop(IList<IDragAndDropEqualityValidator> dragAndDropEqualityValidators, TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            foreach (IDragAndDropEqualityValidator dragAndDropEqualityValidator in dragAndDropEqualityValidators)
            {
                if (!dragAndDropEqualityValidator.IsValid(draggedTreeNavigationItem, targetTreeNavigationItem))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
