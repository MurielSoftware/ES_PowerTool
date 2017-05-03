using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Desktop.Shared.Core.Dtos
{
    [Serializable]
    public abstract class BaseDto
    {
        [Browsable(false)]
        [XmlAttribute("Id")]
        public Guid Id { get; set; }
    }
}
