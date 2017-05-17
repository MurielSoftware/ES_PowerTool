using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Attributes
{
    public class ReferenceEdirorAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Assembly { get; private set; }
        public string CompleteAssembly { get { return Name + "," + Assembly; } }

        public ReferenceEdirorAttribute(string name, string assembly)
        {
            Name = name;
            Assembly = assembly;
        }
    }
}
