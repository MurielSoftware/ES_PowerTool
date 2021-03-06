﻿using Desktop.Shared.Core;
using Desktop.Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class Folder : BaseEntity, IProjectAwareEntity, IStateAwareEntity
    {
        public virtual int SortValue { get; set; }
        public virtual Guid? ParentId { get; set; }

        [Required]
        public virtual string Name { get; set; }
        public virtual int Type { get; set; }
        public virtual bool BuiltIn { get; set; }
        public virtual bool ContentBuiltIn { get; set; }

        public virtual Guid ProjectId { get; set; }
        public virtual State State { get; set; }              

        [ForeignKey("ParentId")]
        public virtual Folder Parent { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public virtual ICollection<Folder> Folders { get; set; }
        public virtual ICollection<ModelObjectType> ModelObjectTypes { get; set; }
    }
}
