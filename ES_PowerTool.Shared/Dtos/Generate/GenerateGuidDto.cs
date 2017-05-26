using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos.Generate
{
    public class GenerateGuidDto : BaseDto
    {
        public virtual int Count { get; set; }
        public virtual bool Uppercase { get; set; }
        public virtual bool RemoveBrackets { get; set; }
        public virtual string GeneratedGuids { get; set; }
    }
}
