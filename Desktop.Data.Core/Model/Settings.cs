using ES_PowerTool.Shared.Dtos.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class Settings : BaseEntity
    {
        public virtual SettingsSection Section { get; set; }
        public virtual SettingsGroup Group { get; set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
    }
}
