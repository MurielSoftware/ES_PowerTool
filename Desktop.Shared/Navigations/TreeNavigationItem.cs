using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Navigations
{
    public class TreeNavigationItem
    {
        public virtual Guid Id { get; set; }
        public virtual string Label { get; set; }
        public virtual NavigationType Type { get; set; }
    }
}
