using Desktop.App.Core.ModelViews;
using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.ModelViews
{
    public class GenerateWindowModelView : BaseModelView
    {
        public GenerateDto Dto { get; protected set; }

        public GenerateWindowModelView(GenerateDto generateDto) 
            : base("GenerateWindowModelView")
        {
            Dto = generateDto;
        }
    }
}
