﻿using Desktop.Shared.Core;
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
        [Browsable(false)]
        [CSVAttribute("SORT_VALUE", 2)]
        public virtual int SortValue { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_DESCRIPTION)]
        [Required]
        [CSVAttribute("DESCRIPTION", 3)]
        public virtual string Description { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_COMMON_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_UNIQUE_NAME)]
        [Required]
        [CSVAttribute("UNIQUE_NAME", 4)]
        public virtual string UniqueName { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_RUNTIME_ID)]
        [Required]
        [CSVAttribute("RUNTIME_ID", 5)]
        public virtual Guid RuntimeId { get; set; }

        [CSVAttribute("DTYPE", 6)]
        [Browsable(false)]
        public virtual string Dtype { get; set; }

        [Browsable(false)]
        [CSVAttribute("FOLDER_ID", 7)]
        public virtual Guid FolderId { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_BUILT_IN)]
        [CSVAttribute("BUILT_IN", 8)]
        public virtual bool BuiltIn { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_DERIVABLE)]
        [CSVAttribute("DERIVABLE", 9)]
        public virtual bool? Derivable { get; set; }

        [Browsable(false)]
        [CSVAttribute("INSTANCE_TYPE", 10)]
        public virtual string InstanceType { get; set; }

        [Browsable(false)]
        [CSVAttribute("DEFAULT_PRESET_ID", 11)]
        public virtual Guid? DefaultPresetId { get; set; }

        [Browsable(false)]
        [CSVAttribute("DEFAULT_WIDGET_TYPE_ID", 12)]
        public virtual Guid? DefaultWidgetTypeId { get; set; }

        [Browsable(false)]
        [CSVAttribute("VERSION", 13)]
        public virtual int Version { get; set; }

        [Browsable(false)]
        public virtual Guid ProjectId { get; set; }

        [Browsable(false)]
        public virtual State State { get; set; }

        public ModelObjectTypeDto()
        {
            InstanceType = null;
            Version = 0;
        }
    }
}
