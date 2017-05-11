using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Model
{
    public class CompositeType : BaseEntity
    {
        public virtual string UniqueName { get; set; }
        public virtual string Description { get; set; }
        public virtual Guid RuntimeId { get; set; }
        public virtual bool BuiltIn { get; set; }
        public virtual int SortValue { get; set; }

        public virtual Guid FolderId { get; set; }

        [ForeignKey("FolderId")]
        public virtual Folder Folder { get; set; }
    }
}
