using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Model
{
    public class Folder : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual bool BuiltIn { get; set; }
        public virtual bool ContentBuiltIn { get; set; }
        public virtual int SortValue { get; set; }

        public virtual Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Folder Parent { get; set; }

        public virtual ICollection<Folder> Folders { get; set; }
        public virtual ICollection<CompositeType> CompositeTypes { get; set; }
    }
}
