﻿using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Dtos
{
    public class CompositeTypeDto : BaseDto
    {
        public virtual string UniqueName { get; set; }
        public virtual string Description { get; set; }
        public virtual Guid RuntimeId { get; set; }
        public virtual bool BuiltIn { get; set; }

        public virtual Guid FolderId { get; set; }
    }
}
