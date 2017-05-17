using Desktop.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Model
{
    public interface IStateAwareEntity
    {
        State State { get; set; }
    }
}
