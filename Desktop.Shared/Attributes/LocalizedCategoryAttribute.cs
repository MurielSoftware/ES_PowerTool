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
        public int SortValue { get; private set; }

        public LocalizedCategoryAttribute(string resourceKey, int sortValue)
            : base()
        {
            _resourceKey = resourceKey;
            SortValue = sortValue;
        }

        protected override string GetLocalizedString(string value)
        {
            return ResourceUtils.GetMessage(_resourceKey);
        }
    }
}
