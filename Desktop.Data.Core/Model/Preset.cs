using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public class Preset : BaseEntity
    {
        public virtual string Name { get; set; }
      
        public virtual Guid CompositeTypeId { get; set; }

        [ForeignKey("CompositeTypeId")]
        public virtual CompositeType CompositeType { get; set; }
    }
}
