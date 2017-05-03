using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    public class ImportWizardDto : BaseDto
    {
        public string PathFolder { get; set; }
        public string PathType { get; set; }
        public string PathTypeElement { get; set; }
    }
}
