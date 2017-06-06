using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Events
{
    public interface IListener<T>
    {
        void OnEvent(T e);
    }
}
