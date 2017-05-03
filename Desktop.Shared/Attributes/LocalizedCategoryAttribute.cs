using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Attributes
{
    public class LocalizedCategoryAttribute : CategoryAttribute
    {
        private readonly string _resourceKey;

        public LocalizedCategoryAttribute(string resourceKey)
            : base()
        {
            _resourceKey = resourceKey;
        }

        protected override string GetLocalizedString(string value)
        {
            return ResourceUtils.GetMessage(_resourceKey);
        }
    }
}
