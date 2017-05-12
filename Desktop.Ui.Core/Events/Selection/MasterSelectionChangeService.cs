using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Ui.Core.Events.Selection
{
    public class MasterSelectionChangeService
    {
        private object _selectedObject;
        private static SelectionChangeNotifier _selectionChangeNotifier = new SelectionChangeNotifier();
        private static Dictionary<string, List<string>> SOURCE_TO_LISTENER_VIEWS = CreateSourceToListenerViewsMap();

        public MasterSelectionChangeService()
        {
        }

        public object GetSelectedObject()
        {
            return _selectedObject;
        }

        public void AddSelectionChangeListener(ISelectionChangeListener selectionChangeListener)
        {
            _selectionChangeNotifier.AddListener(selectionChangeListener);
        }

        public void RemoveSelectionChangeListener(ISelectionChangeListener selectionChangeListener)
        {
            _selectionChangeNotifier.RemoveListener(selectionChangeListener);
        }

        public void SelectionChanged(string sourceViewId, object selection)
        {
            if (!SOURCE_TO_LISTENER_VIEWS.ContainsKey(sourceViewId))
            {
                return;
            }
            _selectedObject = selection;
            _selectionChangeNotifier.FireSelectionChange(selection);
        }

        private static Dictionary<string, List<string>> CreateSourceToListenerViewsMap()
        {
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            map.Add("GameObjectModelView", new List<string>() { "PropertiesModelView" });
            map.Add("ResourceModelView", new List<string>() { "PropertiesModelView" });
            return map;
        }
    }
}
