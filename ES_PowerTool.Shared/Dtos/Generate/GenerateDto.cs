using Desktop.Shared.Core.Dtos;

namespace ES_PowerTool.Shared.Dtos.Generate
{
    public class GenerateDto : BaseDto
    {
        public virtual string GeneratedCSVFolder { get; set; }
        public virtual string GeneratedCSVType { get; set; }
        public virtual string GeneratedCSVTypeElement { get; set; }
        public virtual string GeneratedCSVPreset { get; set; }
        public virtual string GeneratedCSVDefaultPreset { get; set; }
        public virtual string GeneratedCSVTypeType { get; set; }
    }
}
