using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.DataTypes;
using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ES_PowerTool.Shared.Dtos
{
    [Serializable]
    [XmlRoot("Project")]
    [LocalizedDisplayName(MessageKeyConstants.LABEL_PROJECT)]
    public class ProjectDto : BaseDto
    {
        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_NAME)]
        [Required]
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_CSV_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_FOLDER_CSV_PATH)]
        public FilePath PathFolder { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_CSV_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_TYPE_CSV_PATH)]
        public FilePath PathType { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_CSV_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_TYPE_ELEMENT_CSV_PATH)]
        public FilePath PathTypeElement { get; set; }
    }
}
