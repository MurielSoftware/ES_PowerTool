using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Navigations
{
    [Serializable]
    public class CompositePresetElementTreeNavigationItem : TreeNavigationItem
    {
        public virtual string CompositeTypeElementName { get; set; }
        public virtual string CompositeTypeElementElementTypeName { get; set; }
        public virtual string AssociatedPresetName { get; set; }

        public override string ToString()
        {
            return CompositeTypeElementName + " : " + CompositeTypeElementElementTypeName + " -> " + AssociatedPresetName;
        }
    }
}
