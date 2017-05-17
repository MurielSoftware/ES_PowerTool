using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Events.Selection
{
    public interface IServerChangedListener
    {
        void OnServerSwitched(object obj);
    }
}
