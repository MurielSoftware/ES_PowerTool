using Desktop.Shared.Core.Attributes;
using System;

namespace ES_PowerTool.Shared.Dtos.Generate
{
    public class JoinTypeTypeGenerateDto
    {
        [CSVAttribute("SUB_TYPE_ID", 0)]
        public Guid SubTypeId { get; set; }

        [CSVAttribute("SUPER_TYPE_ID", 1)]
        public Guid SuperTypeId { get; set; }

        [CSVAttribute("SORT_VALUE", 2)]
        public int SortValue { get; set; }
    }
}
