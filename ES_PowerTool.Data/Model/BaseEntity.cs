using Desktop.Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Model
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
