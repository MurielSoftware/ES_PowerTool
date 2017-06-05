using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4N.Logger.Specific
{
    class ConsoleLog : ISpecificLog
    {
        public ConsoleLog()
        {
        }

        public void Dispose()
        {
        }

        public void Write(string message)
        {
            Console.WriteLine(message);            
        }
    }
}
