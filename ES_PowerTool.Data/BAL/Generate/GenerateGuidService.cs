using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Services.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Shared.Dtos.Generate;

namespace ES_PowerTool.Data.BAL.Generate
{
    public class GenerateGuidService : BaseService, IGenerateGuidService
    {
        public GenerateGuidService(Connection connection) 
            : base(connection)
        {
        }

        public GenerateGuidDto GenerateGuids(GenerateGuidDto generateGuidDto)
        {
            List<string> guids = DoGenerateGuids(generateGuidDto.Count);

            if(generateGuidDto.Uppercase)
            {
                DoUpperCase(guids);
            }

            if(generateGuidDto.RemoveBrackets)
            {
                DoRemoveBrackets(guids);
            }
            generateGuidDto.GeneratedGuids = BuildString(guids);
            return generateGuidDto;
        }
        private List<string> DoGenerateGuids(int count)
        {
            List<string> guids = new List<string>();
            for (int i = 0; i < count; i++)
            {
                guids.Add(Guid.NewGuid().ToString());
            }
            return guids;
        }

        private void DoUpperCase(List<string> guids)
        {
            guids.ForEach(x => x = x.ToUpper());
        }

        private void DoRemoveBrackets(List<string> guids)
        {
            guids.ForEach(x => x = x.Replace("-", ""));
        }

        private string BuildString(List<string> guids)
        {
            StringBuilder stringBuilder = new StringBuilder();
            guids.ForEach(x => stringBuilder.AppendLine(x));
            return stringBuilder.ToString();
        }
    }
}
