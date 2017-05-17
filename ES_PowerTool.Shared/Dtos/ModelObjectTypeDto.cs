using Desktop.Shared.Core;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    public class ModelObjectTypeDto : BaseDto
    {        
        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_UNIQUE_NAME)]
        [Required]
        [CSVAttribute("UNIQUE_NAME")]
        public virtual string UniqueName { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_DESCRIPTION)]
        [Required]
        [CSVAttribute("DESCRIPTION")]
        public virtual string Description { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_RUNTIME_ID)]
        [Required]
        [CSVAttribute("RUNTIME_ID")]
        public virtual Guid RuntimeId { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_BUILT_IN)]
        [CSVAttribute("BUILT_IN")]
        public virtual bool BuiltIn { get; set; }

        [Browsable(false)]
        [CSVAttribute("SORT_VALUE")]
        public virtual int SortValue { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_DERIVABLE)]
        [CSVAttribute("DERIVABLE")]
        public virtual bool? Derivable { get; set; }

        [Browsable(false)]
        [CSVAttribute("FOLDER_ID")]
        public virtual Guid FolderId { get; set; }

        [CSVAttribute("DTYPE")]
        [Browsable(false)]
        public virtual string Dtype { get; set; }

        [Browsable(false)]
        public virtual State State { get; set; }
    }
}
