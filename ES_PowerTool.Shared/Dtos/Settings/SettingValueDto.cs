using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos.Settings
{
    public class SettingValueDto
    {
        public virtual SettingsSection Section { get; set; }
        public virtual SettingsGroup Group { get; set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
        public virtual bool Changed { get; set; }
    }
}
