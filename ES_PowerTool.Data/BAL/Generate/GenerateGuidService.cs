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
                guids = DoUpperCase(guids);
            }

            if(generateGuidDto.RemoveBrackets)
            {
                guids = DoRemoveBrackets(guids);
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

        private List<string> DoUpperCase(List<string> guids)
        {
            List<string> upperGuids = new List<string>();
            foreach(string guid in guids)
            {
                upperGuids.Add(guid.ToUpper());
            }
            return upperGuids;
        }

        private List<string> DoRemoveBrackets(List<string> guids)
        {
            List<string> guidsWithoutBrackets = new List<string>();
            foreach (string guid in guids)
            {
                guidsWithoutBrackets.Add(guid.Replace("-", ""));
            }
            return guidsWithoutBrackets;
        }

        private string BuildString(List<string> guids)
        {
            StringBuilder stringBuilder = new StringBuilder();
            guids.ForEach(x => stringBuilder.AppendLine(x));
            return stringBuilder.ToString();
        }
    }
}
