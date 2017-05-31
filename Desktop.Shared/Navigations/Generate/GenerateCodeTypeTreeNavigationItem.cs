using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Navigations.Generate
{
    public class GenerateCodeTypeTreeNavigationItem : TreeNavigationItem
    {
        //public virtual string TableName { get; set; }
        public virtual bool Table { get; set; }
        public virtual bool DiscriminatorValue { get; set; }
        public virtual string Namespace { get; set; }
        public virtual string DtoGenerated { get; set; }
        public virtual string EntityGenerated { get; set; }

        public virtual ICollection<GenerateCodeTypeElementTreeNavigationItem> Fields { get; set; }

        public GenerateCodeTypeTreeNavigationItem()
        {
            Fields = new List<GenerateCodeTypeElementTreeNavigationItem>();
        }
    }
}
