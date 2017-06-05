using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4N.Logger.Specific
{
    interface ISpecificLog : IDisposable
    {
        void Write(string message);
    }
}
