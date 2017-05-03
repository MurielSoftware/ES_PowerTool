using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Model
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
    }
}
