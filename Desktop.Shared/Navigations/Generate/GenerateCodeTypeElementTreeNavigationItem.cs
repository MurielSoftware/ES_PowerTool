using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Navigations.Generate
{
    public class GenerateCodeTypeElementTreeNavigationItem : GenerateLiquibaseCompositeTypeElementTreeNavigationItem
    {
        public virtual bool Id { get; set; }
        public virtual bool Transient { get; set; }
    }
}
