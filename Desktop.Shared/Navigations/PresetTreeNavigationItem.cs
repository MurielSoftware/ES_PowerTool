using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Navigations
{
    [Serializable]
    public class PresetTreeNavigationItem : TreeNavigationItem
    {
        public bool IsDefault { get; set; }
    }
}
