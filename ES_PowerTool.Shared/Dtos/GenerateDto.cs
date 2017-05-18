using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    public class GenerateDto : BaseDto
    {
        public virtual string GeneratedCSVFolder { get; set; }
        public virtual string GeneratedCSVType { get; set; }
        public virtual string GeneratedCSVTypeElement { get; set; }
    }
}
