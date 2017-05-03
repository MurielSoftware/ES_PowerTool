using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Attributes
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string _resourceKey;

        public LocalizedDisplayNameAttribute(string resourceKey)
            : base()
        {
            _resourceKey = resourceKey;
        }

        public override string DisplayName
        {
            get
            {
                return ResourceUtils.GetMessage(_resourceKey);
            }
        }
    }
}
