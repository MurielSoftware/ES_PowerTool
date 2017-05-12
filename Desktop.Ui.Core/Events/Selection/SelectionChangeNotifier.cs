using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Ui.Core.Events.Selection
{
    public class SelectionChangeNotifier
    {
        private List<ISelectionChangeListener> _selectionChangeListeners = new List<ISelectionChangeListener>();

        public void AddListener(ISelectionChangeListener selectionChangeListener)
        {
            _selectionChangeListeners.Add(selectionChangeListener);
        }

        public void RemoveListener(ISelectionChangeListener selectionChangeListener)
        {
            _selectionChangeListeners.Remove(selectionChangeListener);
        }

        public void FireSelectionChange(object selection)
        {
            foreach(ISelectionChangeListener selectionChangeListener in _selectionChangeListeners)
            {
                selectionChangeListener.OnSelectionChange(selection);
            }
        }
    }
}
