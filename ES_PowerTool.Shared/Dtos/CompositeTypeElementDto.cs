﻿using Desktop.Shared.Core;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.DataTypes;
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
    [LocalizedDisplayName(MessageKeyConstants.LABEL_TYPE_ELEMENT)]
    public class CompositeTypeElementDto : BaseDto
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
        [LocalizedDisplayName(MessageKeyConstants.LABEL_OPTIONAL)]
        [CSVAttribute("OPTIONAL")]
        public virtual bool Optional { get; set; }
        
        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_RUNTIME_ID)]
        [Required]
        [CSVAttribute("RUNTIME_ID")]
        public virtual Guid RuntimeId { get; set; }

        [Browsable(false)]
        [CSVAttribute("SORT_VALUE")]
        public virtual int SortValue { get; set; }

        [Browsable(false)]
        [CSVAttribute("OWNING_TYPE_ID")]
        public virtual Guid OwningTypeId { get; set; }

        [Browsable(false)]
        [CSVAttribute("ELEMENT_TYPE_ID")]
        public virtual Guid ElementTypeId { get; set; }

        [LocalizedCategory(MessageKeyConstants.LABEL_SPECIFIC_CATEGORY)]
        [LocalizedDisplayName(MessageKeyConstants.LABEL_ELEMENT_TYPE)]
        [Reference("ElementType")]
        [ReferenceEdiror("ES_PowerTool.Editors.TypeReferenceEditor", "ES_PowerTool")]
        //[Editor("Editor.Ui.Editors.ModelReferenceEditor, Editor", "Editor.Core.Ui.Editors.BaseReferenceEditor, Editor.Core")]
        public virtual ReferenceString ModelReference { get; set; }

        [Browsable(false)]
        public virtual State State { get; set; } 
    }
}