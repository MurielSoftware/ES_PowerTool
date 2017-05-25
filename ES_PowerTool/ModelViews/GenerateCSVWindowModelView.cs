using Desktop.App.Core.ModelViews;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Dtos.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.ModelViews
{
    public class GenerateCSVWindowModelView : BaseModelView
    {
        public GenerateDto Dto { get; protected set; }

        public GenerateCSVWindowModelView(GenerateDto generateDto) 
            : base("GenerateWindowModelView")
        {
            Dto = generateDto;
        }
    }
}
