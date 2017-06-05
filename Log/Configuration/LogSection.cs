using Log4N.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4N.Configuration
{
    public class LogSection : ConfigurationSection
    {
        [ConfigurationProperty("target", IsRequired = true)]
        public LogTarget Target
        {
            get { return (LogTarget)this["target"]; }
            set { this["target"] = value; }
        }

        [ConfigurationProperty("file", IsRequired = false)]
        public string File
        {
            get { return (string)this["file"]; }
            set { this["file"] = value; }
        }
    }
}
